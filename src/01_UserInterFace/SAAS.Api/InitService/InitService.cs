using ConfigurationService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OauthService.Oauth.AuthHelper;
using SwaggerService;
using SwaggerService.Filters;
using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using OauthService.OauthConfig;
using SysBase.Domain;
using Microsoft.EntityFrameworkCore;
using SysBase.Domain.Models;
using SAAS.FrameWork.IOC;

namespace SAAS.Api
{
    public  class InitService
    {

        #region 依赖注入服务及相关依赖项初始化
        public static void Init(IServiceCollection services, IConfiguration Configuration)
        {
            ConfigureRedisRepositoryService(services, Configuration);//配置缓存和自动生成ID
            ConfigureJWTAuthenticationService(services, Configuration);//配置JWT认证

           
        }
        #endregion
        

        /// <summary>
        /// 数据库自动生成ID注入
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureRedisRepositoryService(IServiceCollection services, IConfiguration Configuration)
        {
            var sqlconnction = Configuration.GetSection(nameof(ConnectionStrings));
            services.AddDbContextPool<SysBaseDbContext>(options =>
            {
                options.UseSqlServer(sqlconnction[nameof(ConnectionStrings.Sys_Db)]);
            });
            services.AddSingleton(item => new IdWorker(1, Configuration.GetValue<int>("ObjAttr:ServerNodeNo")));
        }
        /// <summary>
        /// 配置JWT身份认证--配置对象注入（Ioption）
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureJWTAuthenticationService(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<IJwtFactory, JwtFactory>();
            #region Jwt token Authentication

            var jwtAppSettingOptions = Configuration.GetSection(nameof(Jwt));
       
            var _signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtAppSettingOptions[nameof(Jwt.SecurityKey)]));
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(Jwt.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(Jwt.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
                options.ExpireMinutes = int.Parse(jwtAppSettingOptions[nameof(Jwt.ExpireMinutes)]);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(Jwt.Issuer)];
                configureOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtAppSettingOptions[nameof(Jwt.Issuer)],
                    ValidateAudience = true,
                    ValidAudience = jwtAppSettingOptions[nameof(Jwt.Audience)],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _signingKey,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                configureOptions.SaveToken = true;
            });
            #endregion

            #region Authorization
            services.AddAuthorization(options =>
            {



                // options.AddPolicy("APIAccess", policy => policy.RequireClaim(ClaimTypes.Role, "api_access"));
                options.AddPolicy("Administrator", policy => policy.RequireRole("管理员"));
                options.AddPolicy("Administrator", policy => policy.RequireClaim(ClaimTypes.Role, "管理员"));
                options.AddPolicy("Permission", policy => policy.Requirements.Add(new PermissionRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton<IAuthorizationHandler, ResourceAuthorizationHandler>();
            #endregion
        }
        /// <summary>
        /// 配置身份认证
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureAuthenticationService(IServiceCollection services, IConfiguration Configuration)
        {

            #region  认证服务器-将资源和客户端的初始信息服务加入到DI容器
            services.AddIdentityServer()//添加认证服务器
          .AddDeveloperSigningCredential()//加密算法
          .AddInMemoryApiResources(Config.GetApiResources()).AddInMemoryIdentityResources(Config.GetIdentityResources())  //配置资源
          .AddInMemoryClients(Config.GetClients()).AddTestUsers(Config.GetTestUsers()); //配置用户密码的默认测试用户类似超级用户;   
            #endregion
            #region 客户端服务器-添加认证服务器地址
            services.AddAuthentication("Bearer")
               .AddIdentityServerAuthentication(options =>
               {
                   options.Authority = "http://localhost:4823";    //配置Identityserver的授权地址
                   options.RequireHttpsMetadata = false;           //不需要https    
                   options.ApiName = "api";                        //api的name，需要和config的名称相同
                   options.ApiSecret = "apipwd";  //对应ApiResources中的密钥
                   options.JwtValidationClockSkew = TimeSpan.FromSeconds(0);//过期时间还能有多长时间偏移量

               });


            #endregion
        }
        /// <summary>
        /// 项目接口文档配置
        /// </summary>
        /// <returns></returns>
        public static CustsomSwaggerOptions GetSwaggerOpntion()
        {
            CustsomSwaggerOptions CURRENT_SWAGGER_OPTIONS = new CustsomSwaggerOptions()
            {

                ProjectName = "平台接口文档",
                //ApiVersions = new string[] { "v1", "v2" },
                UseCustomIndex = true,
                RoutePrefix = "swagger",
                SwaggerAuthList = new List<CustomSwaggerAuth>()
            {
                new CustomSwaggerAuth("sam","123456")
            },
                AddSwaggerGenAction = c =>
                {
                    c.OperationFilter<AssignOperationVendorFilter>();
                    var filePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml");
                    //controller及action注释
                    c.IncludeXmlComments(filePath, true);
                },
                UseSwaggerAction = c =>
                {

                },
                UseSwaggerUIAction = c =>
                {
                }
            };
            return CURRENT_SWAGGER_OPTIONS;
        }
        /// <summary>
        /// 查找程序集服务注册接口-复杂对象会报错
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        private static void ConfigureAssemblyRepositoryService(IServiceCollection services, IConfiguration Configuration)
        {
            #region Dependency Injection Services

            //Load assembly from appsetting.json
            //
            string assemblies = Configuration["Assembly:FunctionAssembly"];

            if (!string.IsNullOrEmpty(assemblies))
            {
                foreach (var item in assemblies.Split('|'))
                {
                    Assembly assembly = Assembly.Load(item);
                    foreach (var implement in assembly.GetTypes())
                    {
                        Type[] interfaceType = implement.GetInterfaces();
                        foreach (var service in interfaceType)
                        {
                            services.AddTransient(service, implement);
                        }
                    }
                }
            }

            #endregion
        }
        /// <summary>
        /// 查找程序集服务注册接口
        /// </summary>
        /// <param name="services"></param>
        /// <param name="solutionPrefix"></param>
        /// <param name="projectSuffixes"></param>
        public static void ResolveAllTypes( IServiceCollection services, string solutionPrefix, params string[] projectSuffixes)
        {
            //solutionPrefix 解决方案名称
            //projectSuffixes 需要扫描的项目名称集合
            //注意: 如果使用此方法，必须提供需要扫描的项目名称

            var allAssemblies = new List<Assembly>();
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (var dll in Directory.GetFiles(path, "*.dll"))
                allAssemblies.Add(Assembly.LoadFile(dll));


            var types = new List<Type>();
            foreach (var assembly in allAssemblies)
            {
                if (assembly.FullName.StartsWith(solutionPrefix))
                {
                    foreach (var assemblyDefinedType in assembly.DefinedTypes)
                    {
                        if (projectSuffixes.Any(x => assemblyDefinedType.Name.EndsWith(x)))
                        {
                            types.Add(assemblyDefinedType.AsType());

                        }
                    }
                }
            }

            var implementTypes = types.Where(x => x.IsClass).ToList();
            foreach (var implementType in implementTypes)
            {
                //接口和实现的命名规则为："AService"类实现了"IAService"接口,你也可以自定义规则
                var interfaceType = implementType.GetInterface("I" + implementType.Name);

                if (interfaceType != null)
                {
                    services.Add(new ServiceDescriptor(interfaceType, implementType,
                        ServiceLifetime.Scoped));
                }

            }

        }

    }
}
