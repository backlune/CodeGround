using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;

namespace CodeGround.WebCore
{
   public class Startup
   {
      public Startup(IHostingEnvironment env)
      {
         var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
             .AddEnvironmentVariables();
         Configuration = builder.Build();
      }

      public IConfigurationRoot Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         // Add framework services.
         services.AddMvc()
          .AddApplicationPart(Assembly.GetExecutingAssembly())
          .AddControllersAsServices();


         // Add swagger
         //services.AddSwaggerGen();

      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
      {
         loggerFactory.AddConsole(Configuration.GetSection("Logging"));
         loggerFactory.AddDebug();

         app.UseMvc();

         //Setup swagger
         app.UseSwagger(Assembly.GetExecutingAssembly(), new SwaggerOwinSettings()
         {
            DefaultPropertyNameHandling = NJsonSchema.PropertyNameHandling.CamelCase,
           // Title = "MyApi",
            Version = "1.0"

         });

         app.UseSwaggerUi(Assembly.GetExecutingAssembly(), new SwaggerUiOwinSettings());
      }
   }
}
