using System;
using System.Linq;
using System.Reflection;
using Autofac;
using CommonInterface;
using SAAS.InfrastructureCore;
using SAAS.InfrastructureCore.Autofac;

namespace User.Service
{
    /// <summary>
    /// 实现依赖注入接口(autofac)
    /// </summary>
    public class DependencyAutofacRegistrar : IDependencyAutofacRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            var allType = Assembly.GetExecutingAssembly();//获取当前运行的程序集
            builder.RegisterAssemblyTypes(allType)
           .Where(a => a.IsClass && a.GetInterfaces().Contains(typeof(AutoInject)))
            .AsImplementedInterfaces().InstancePerLifetimeScope();
             //builder.RegisterType<DemoService>().As<IDemoService>().SingleInstance();
        }

       
    }
}
