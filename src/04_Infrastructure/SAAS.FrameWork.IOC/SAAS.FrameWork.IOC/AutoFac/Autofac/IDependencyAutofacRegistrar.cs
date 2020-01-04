using Autofac;
using SAAS.InfrastructureCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAAS.InfrastructureCore.Autofac
{
    /// <summary>
    /// 依赖注入接口
    /// </summary>
    public interface IDependencyAutofacRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);
    }
}
