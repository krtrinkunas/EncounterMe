using Api.Aspects;
using Api.Middleware;
using Api.ModelContext;
using Api.Repositories;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using EncounterMeApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public ILifetimeScope AutofacContainer { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddScoped<ILocationRepository, LocationRepository>();
            //services.AddDbContext<LocationContext>(p => p.UseSqlite("Data source=locations.db"));

            services.AddScoped<IPlayerRepository, PlayerRepository>();
            //services.AddDbContext<PlayerContext>(o => o.UseSqlite("Data source=players.db"));

            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<ICommentRatingRepository, CommentRatingRepository>();

            services.AddScoped<ILocationRatingRepository, LocationRatingRepository>();

            services.AddDbContext<DatabaseContext>(o => o.UseSqlite("Data source=EncounterMeDB.db"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            builder.RegisterType<InternetLocationService>().As<ILocationService>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LogAspect))
                .InstancePerDependency();

            builder.RegisterType<InternetPlayerService>().As<IPlayerService>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LogAspect))
                .InstancePerDependency();

            builder.RegisterType<InternetCommentService>().As<ICommentService>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LogAspect))
                .InstancePerDependency();

            builder.RegisterType<InternetCommentRatingService>().As<ICommentRatingService>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LogAspect))
                .InstancePerDependency();

            builder.RegisterType<InternetLocationRatingService>().As<ILocationRatingService>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LogAspect))
                .InstancePerDependency();

            //builder.RegisterType<WeatherServiceAgent>().As<IWeatherServiceAgent>().InstancePerDependency();
            builder.Register(x => Log.Logger).SingleInstance();
            builder.RegisterType<LogAspect>().SingleInstance();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
                app.UseHttpsRedirection();
            }

            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<StatisticsMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
