using System;
using System.Linq;
using AutoMapper;
using CacheSqlXmlService;
using Demo.Service.DtoValidators;
using ExceptionMiddleService.DoTime;
using ExceptionService;
using FluentValidation.AspNetCore;
using Mapper.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OauthService.Models.AuthContext;
using Quartz;
using Quartz.Impl;
using SAAS.Api.Validate;
using SAAS.FrameWork.Common;
using SAAS.FrameWork.IOC;
using SAAS.InfrastructureCore;
using SwaggerService;
using SwaggerService.Builder;
using SysBase.Domain;
using TaskService.Extensions;

namespace SAAS.Api
{
    public class Startup
    {
        /// <summary>
        /// 启动项
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //获取引擎上下文实例
            this.Engine = EngineContext.Current;
        }
        /// <summary>
        /// 全局配置项
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 定义服务引擎
        /// </summary>
        public IEngine Engine { get; private set; }
        /// <summary>
        /// 项目接口文档配置
        /// </summary>
        private CustsomSwaggerOptions CURRENT_SWAGGER_OPTIONS = InitService.GetSwaggerOpntion();
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
             services.AddCors(o =>
                o.AddPolicy("*",
                    builder => builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials()
                ));
            #region (01)服务注册-初始化基础服务，配置缓存和自动生成ID，配置JWT认证，加入缓存对象
            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddSingleton<IPathProvider, PathProvider>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddAutoMapper(typeof(MapperProfiles));
            InitService.Init(services, Configuration);
            #endregion
            #region  (02)服务注册-添加总控异常处理拦截器动态注入验证类和依赖注入服务层和仓储层
            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidateModelAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.Converters.Add(new IdToStringConverter());
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();  //options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;//循环依赖的问题或者标记[JsonIgnore]
            });
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DemoModleValidator>());//加入验证类  ;
            #endregion   
            #region (03)服务注册-Swagger版本控制
            services.AddMvcCore().AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");
            services.AddApiVersioning(option =>
            {
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = false;
            });
            services.AddCustomSwagger(CURRENT_SWAGGER_OPTIONS);
            #endregion
            #region (04)AUTOFAC动态注入服务
          var serviceProvider= this.Engine.ConfigureServices(services); //动态注入业务服务
           
            return serviceProvider;
            #endregion
        }

        /// <summary>
        /// 处理服务中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        /// <param name="cache"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider, Microsoft.Extensions.Caching.Memory.IMemoryCache cache)
        {
            app.UseCalculateExecutionTime();//只需在此添加
            #region (01)开发版本启用错误页面
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();//开发环境启用错误页面
            }
            #endregion
            #region (02)中间件-认证

            app.UseAuthentication();// 添加认证  //app.UseIdentityServer();// 添加认证服务器

            #endregion
            #region (03)异常处理及用户认证对象静态缓存
            app.ConfigureCustomExceptionMiddleware();
            var serviceProvider = app.ApplicationServices;
            var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            AuthContextService.Configure(httpContextAccessor);
            #endregion
            #region (04)中间件-配置本资源服务器及跨域访问
            app.UseCors("*");
            #endregion
            #region (05)中间件-自定义cache服务以及依赖注入的容器类
            CacheFactory.Init(cache);//注册自定义cache服务给sqlxml用
            #endregion
            #region (06)中间件-Swagger自动检测存在的版本
            CURRENT_SWAGGER_OPTIONS.ApiVersions = provider.ApiVersionDescriptions.Select(s => s.GroupName).ToArray();
            app.UseCustomSwagger(CURRENT_SWAGGER_OPTIONS);
            app.UseQuartz(env).UseStaticHttpContext();//执行本地job
            #endregion
            #region (07)启用WWW静态站点及MVC使用
            app.UseStaticFiles();//允许访问静态资源
            app.UseMvc();
            #endregion
        }




    }
}
