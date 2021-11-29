using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Middleware
{
    public class StatisticsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public StatisticsMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var controllerActionDescriptor =
                context
                .GetEndpoint()
                .Metadata
                .GetMetadata<ControllerActionDescriptor>();

            var controllerName = controllerActionDescriptor.ControllerName;
            var actionName = controllerActionDescriptor.ActionName;

            await _next(context);

            sw.Stop();

            _logger.Information($"It took {sw.ElapsedMilliseconds} ms to perform " +
                $"this action {actionName} in this controller {controllerName}");
        }
    }
}
