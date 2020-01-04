using System.Linq;
using Autofac;
using SAAS.InfrastructureCore.Autofac;
using SAAS.InfrastructureCore;
using SAAS.Framework.Orm.EfCore.Repositories;
using System.Reflection;

namespace DemoEF.Domain
{
    /// <summary>
    /// 实现依赖注入接口
    /// </summary>
    public class DependencyAutofacRegistrar : IDependencyAutofacRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            var allType = Assembly.GetExecutingAssembly();//获取默认程序集
            builder.RegisterAssemblyTypes(allType);
            builder.RegisterGeneric(typeof(EFRepository<>)).As(typeof(IEFRepository<>)).InstancePerDependency();//程序集注入，也可做拦截方法
        }
    }
}
