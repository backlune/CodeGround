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
using NSwag.CodeGeneration.SwaggerGenerators.WebApi.Processors;
using NSwag.CodeGeneration.SwaggerGenerators.WebApi.Processors.Contexts;
using Microsoft.AspNetCore.Http;
using NJsonSchema;
using NSwag;

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
         var setting = new SwaggerOwinSettings()
         {
            DefaultPropertyNameHandling = NJsonSchema.PropertyNameHandling.CamelCase,
            Title = "Upload2",
            Version = "1.0",

         };

         setting.OperationProcessors.Add(new MyOperationProcessor());
         app.UseSwagger(Assembly.GetExecutingAssembly(), setting);




         var uiSettings = new SwaggerUiOwinSettings();
         //uiSettings.OperationProcessors.Add(new MyOperationProcessor());
         app.UseSwaggerUi(Assembly.GetExecutingAssembly(), uiSettings);
      }
   }

   public class MyOperationProcessor : IOperationProcessor
   {
      #region Implementation of IOperationProcessor

      public Task<bool> ProcessAsync(OperationProcessorContext context)
      {
         return Task.Run(() =>
         {
            var attrib =
               (MultiFormFileDataAttribute) context.MethodInfo.GetCustomAttribute(typeof(MultiFormFileDataAttribute));

            if (attrib == null)
               return true;

            var operationParameter = new SwaggerParameter()
            {
               Name = attrib.Name,
               Kind = SwaggerParameterKind.FormData,
               Type = JsonObjectType.File,
               IsRequired = true,
               Description = attrib.Documentation
            };

            context.OperationDescription.Operation.Parameters.Add(operationParameter);
            context.OperationDescription.Operation.Consumes = new List<string>() {"multipart/form-data"};

            return true;
         });

      }

      #endregion
   }

   [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
   public class MultiFormFileDataAttribute : Attribute
   {
      public string Name { get; set; }

      public string Documentation { get; set; }
   }
}
