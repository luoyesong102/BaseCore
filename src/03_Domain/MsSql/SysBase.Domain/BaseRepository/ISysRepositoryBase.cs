
using SAAS.Framework.Orm.EfCore.UnitWork;
using SysBase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysBase.Domain
{
    public interface ISysRepositoryBase<TEntity> : IRepository<SysBaseDbContext, TEntity>  where TEntity:class
    {
    }
}
