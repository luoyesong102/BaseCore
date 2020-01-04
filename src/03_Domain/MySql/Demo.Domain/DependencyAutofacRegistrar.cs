using System.Linq;
using System.Reflection;
using Autofac;
using SAAS.DB.Dapper.Database.DbRepository;
using SAAS.InfrastructureCore;
using SAAS.InfrastructureCore.Autofac;

namespace Demo.Domain
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


        }
    }
}
