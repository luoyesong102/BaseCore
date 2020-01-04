using Microsoft.Extensions.DependencyInjection;
using  SwaggerService.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace  SwaggerService.Builder
{
    public static class CustomSwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            return AddCustomSwagger(services, new CustsomSwaggerOptions());
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, CustsomSwaggerOptions options)
        {
            services.AddSwaggerGen(c =>
             {
                 if (options.ApiVersions == null) return;
                 foreach (var version in options.ApiVersions)
                 {
                     c.SwaggerDoc(version, new Info { Title = options.ProjectName, Version = version });
               
                 }
                 c.OperationFilter<SwaggerDefaultValueFilter>();
                 options.AddSwaggerGenAction?.Invoke(c);
                 c.AddSecurityDefinition("Bearer",
                   new ApiKeyScheme
                   {
                       In = "header",
                       Description = "请输入OAuth接口返回的Token，前置Bearer。示例：Bearer {Roken}",
                       Name = "Authorization",
                       Type = "apiKey"
                   });
                 c.AddSecurityRequirement(
                     new Dictionary<string, IEnumerable<string>>
                     {
                        { "Bearer",
                          Enumerable.Empty<string>()
                        },
                     });
             });
            return services;
        }
    }
}
