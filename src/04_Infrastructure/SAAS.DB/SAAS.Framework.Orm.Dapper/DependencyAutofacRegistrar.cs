using System.Linq;
using Autofac;
using SAAS.InfrastructureCore.Autofac;
using SAAS.InfrastructureCore;
using SAAS.DB.Dapper.Database.DbRepository;

namespace SAAS.Framework.Orm.Dapper
{
    /// <summary>
    /// 实现依赖注入接口
    /// </summary>
    public class DependencyAutofacRegistrar : IDependencyAutofacRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency()
        }
    }
}
