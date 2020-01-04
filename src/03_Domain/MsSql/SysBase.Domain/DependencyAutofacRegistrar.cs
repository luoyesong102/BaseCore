using System;
using System.Linq;
using System.Reflection;
using Autofac;

using Microsoft.Extensions.DependencyInjection;
using SAAS.InfrastructureCore;
using SAAS.InfrastructureCore.Autofac;
using SysBase.Domain;
using SysBase.Domain.DomainService;

namespace Demo.Service
{
    /// <summary>
    /// 实现依赖注入接口(autofac)
    /// </summary>
    public class DependencyAutofacRegistrar : IDependencyAutofacRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<UserDomainService>();//transcate
            builder.RegisterType<RoleDomainService>();//transcate
            builder.RegisterType<IconDomainService>();//transcate
            builder.RegisterType<PermissionDomainService>();//transcate
            builder.RegisterType<MenuDomainService>();//transcate
            builder.RegisterType<SysUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(SysRepositoryBase<>)).As(typeof(ISysRepositoryBase<>));//注册基础仓储操作(多泛型不支持)  .InstancePerDependency()     transcate   
            //var allType = Assembly.GetExecutingAssembly();//获取默认程序集
            //builder.RegisterAssemblyTypes(allType);

        }


    }
}
