using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace SAAS.Framework.Orm.EfCore.UnitWork
{
    /// <summary>
    /// 工作单元将事务的操作隔离出来
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        IDbContextTransaction trans;
        TransactionScope transscope;
        public TDbContext DBContext
        {
            get { return _dbContext; }

        }
        public UnitOfWork(TDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }


        #region 事务处理
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {

           trans= _dbContext.Database.BeginTransaction();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTrans()
        {
            trans.Commit();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            trans.Rollback();
        }
        /// <summary>
        /// 开始分布式事务
        /// </summary>
        public void BeginTransactionScope()
        {
            transscope = new TransactionScope();

        }
        /// <summary>
        /// 提交分布式事务
        /// </summary>
        public void AcceptTransactionScope()
        {
            transscope.Complete();

        }
        /// <summary>
        /// 回滚分布式事务
        /// </summary>
        public void DisposeTransactionScope()
        {
            transscope.Dispose();

        }

        #endregion
    }
}
