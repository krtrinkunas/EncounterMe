using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using Serilog;

namespace Api.Aspects
{
    public class LogAspect : Castle.DynamicProxy.IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();

                Log.Logger.Information($"Method {invocation.Method.Name} " +
                    $"called with these parameters: {JsonConvert.SerializeObject(invocation.Arguments)}" +
                    $"returned this response: {JsonConvert.SerializeObject(invocation.ReturnValue)}");
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Error happened in method: {invocation.Method}. Error: {JsonConvert.SerializeObject(ex)}");
                throw;
            }
        }
    }
}
