
using SAAS.Framework.Orm.EfCore.UnitWork;
using SysBase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysBase.Domain
{
    public class SysRepositoryBase<TEntity> : EFRepository<SysBaseDbContext, TEntity>, ISysRepositoryBase<TEntity>  where  TEntity:class
    {
        public SysRepositoryBase(SysBaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
