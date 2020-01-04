using SAAS.InfrastructureCore;
using System;
using System.Collections.Generic;
using System.Text;
using SAAS.Dependency;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using SAAS.FrameWork.Log;

namespace SAAS.InfrastructureCore.Autofac
{
    /// <summary>
    /// Autofac IOC容器
    /// </summary>
    public class AutofacEngine : IEngine
    {
        private IConfigurationRoot _configuration;
        private IHostingEnvironment _hostingEnvironment;
        private ContainerManager _containerManager;
        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
            set { _containerManager = value; }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            throw new NotImplementedException();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            var container = builder.Build();
            DependencyAutofacRegister(container);
            return container.Resolve<IServiceProvider>();
        }

        private void DependencyAutofacRegister(IContainer container)
        {

            Logger.log.Info("开始执行依赖注入.....");
            var typeFinder = new WebAppTypeFinder();
            var builder = new ContainerBuilder();
            try
            {
                //类型查询器依赖注入
                builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
                builder.Update(container);


                //自定义依赖注入
                builder = new ContainerBuilder();
                var dependencyAutofacRegistrar = typeFinder.FindClassesOfType<IDependencyAutofacRegistrar>();
                List<IDependencyAutofacRegistrar> dependencyAutofacRegistrarList = new List<IDependencyAutofacRegistrar>();
                foreach (var dependencyAutofacRegistrarItem in dependencyAutofacRegistrar)
                {
                    dependencyAutofacRegistrarList.Add((IDependencyAutofacRegistrar)Activator.CreateInstance(dependencyAutofacRegistrarItem));
                }
                foreach (var dependencyAutofacRegistrarListItem in dependencyAutofacRegistrarList)
                {
                    Logger.log.Info($"正在注入{dependencyAutofacRegistrarListItem.GetType().FullName}类型");
                    dependencyAutofacRegistrarListItem.Register(builder, typeFinder);
                }
                builder.Update(container);
            }
            catch (Exception ex)
            {
                Logger.log.Info($"依赖注入失败，异常消息：{ex.Message}");
                throw;
            }
        }

        public void Initialize(IConfigurationRoot configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _hostingEnvironment = env;
        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }
    }
}
