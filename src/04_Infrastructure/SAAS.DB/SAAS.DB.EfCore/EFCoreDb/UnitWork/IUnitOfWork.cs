using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAAS.Framework.Orm.EfCore.UnitWork
{
    public interface IUnitOfWork<TDbContext> where TDbContext : DbContext
    {

        #region 事务提交
        /// <summary>
        /// 异步保存
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 当前同一个context的事物上下文事务开始事务
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// 当前同一个context的事物上下文事务提交事务
        /// </summary>
        void CommitTrans();
        /// <summary>
        /// 当前同一个context的事物上下文事务回滚事务
        /// </summary>
        void Rollback();
        /// <summary>
        /// 分布式事务开始事务
        /// </summary>
        void BeginTransactionScope();
        /// <summary>
        /// 分布式事务提交事务
        /// </summary>
        void AcceptTransactionScope();
        /// <summary>
        /// 分布式事务回滚事务
        /// </summary>
        void DisposeTransactionScope();
        #endregion
    }
}
