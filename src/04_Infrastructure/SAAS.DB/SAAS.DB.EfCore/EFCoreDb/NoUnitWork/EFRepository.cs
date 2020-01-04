using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;

using System.Threading.Tasks;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Data.Common;

using System.Text.RegularExpressions;

using EFCore.BulkExtensions;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using SAAS.FrameWork.Util.Common;
using Newtonsoft.Json;

namespace SAAS.Framework.Orm.EfCore.Repositories
{

    /// <summary>
    /// 1基础仓库的操作-带状态追踪或者不带状态追踪--分页查询--批量处理
    /// 2DBContent线程池暂时未提供（同时不是安全线程,所以不能异步共享同一个）
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EFRepository<TDataBase> : IEFRepository<TDataBase> where TDataBase : DbContext, new()
    {
        #region 数据上下文

            /// <summary>
            /// 数据上下文
            /// </summary>
        private TDataBase _Context;
        IDbContextTransaction trans;
        TransactionScope transscope;
        public EFRepository()
        {
            _Context = new TDataBase();
            
           
        }
        public TDataBase DBContext
        {
            get { return _Context; }

        }

        #endregion
        #region 带状态跟踪
        /// <summary>
        /// 获取集合数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public virtual IList<T> Get<T>(
         Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         List<Expression<Func<T, object>>> includes = null) where T : class
        {
            IQueryable<T> query = _Context.Set<T>();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToList();
        }
        /// <summary>
        /// 修改带状态跟踪
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T UpdateState<T>(T entity) where T : class
        {
            AttachIfNot(entity);
            this._Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        /// <summary>
        /// 新增带状态跟踪
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
      public  T SaveState<T>(T entity) where T : class
        {
            var result = _Context.Set<T>().Add(entity);
            return result.Entity;
        }
        /// <summary>
        /// 删除带状态跟踪
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void DeleteState<T>(T entity) where T : class
        {
            AttachIfNot(entity);
            _Context.Set<T>().Remove(entity);
        }
        public int SaveChanges()
        {
            return _Context.SaveChanges();
        }
        protected virtual void AttachIfNot<T>(T entity) where T : class
        {
            if (this._Context.Entry(entity).State == EntityState.Detached)
            {
                _Context.Set<T>().Attach(entity);
            }
        }
        #endregion
        #region 单模型 CRUD 操作

        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool Save<T>(T entity, bool IsCommit = true) where T : class
        {

            _Context.Set<T>().Add(entity);
            if (IsCommit)
                try {
                    return _Context.SaveChanges() > 0;
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    return _Context.SaveChanges() > 0;
                   
                }
            else
                return false;
        }
        /// <summary>
        /// 增加一条记录(异步方式)
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> SaveAsync<T>(T entity, bool IsCommit = true) where T : class
        {
            _Context.Set<T>().Add(entity);
            if (IsCommit)
                try
                {
                    return await Task.Run(() => _Context.SaveChanges() > 0);
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    return await Task.Run(() => _Context.SaveChanges() > 0);

                }
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool Update<T>(T entity, bool IsCommit = true) where T : class
        {
            _Context.Set<T>().Attach(entity);
            _Context.Entry<T>(entity).State = EntityState.Modified;
            if (IsCommit)
                try
                {
                    return _Context.SaveChanges() > 0;
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    return _Context.SaveChanges() > 0;

                }
            else
                return false;
        }
        /// <summary>
        /// 更新一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync<T>(T entity, bool IsCommit = true) where T : class
        {
            _Context.Set<T>().Attach(entity);
            _Context.Entry<T>(entity).State = EntityState.Modified;
            if (IsCommit)
                try
                {
                    return await Task.Run(() => _Context.SaveChanges() > 0);
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    return await Task.Run(() => _Context.SaveChanges() > 0);

                }
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 增加或更新一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsSave">是否增加</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool SaveOrUpdate<T>(T entity, bool IsSave, bool IsCommit = true) where T : class
        {
            return IsSave ? Save(entity, IsCommit) : Update(entity, IsCommit);
        }
        /// <summary>
        /// 增加或更新一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsSave">是否增加</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> SaveOrUpdateAsync<T>(T entity, bool IsSave, bool IsCommit = true) where T : class
        {
            return IsSave ? await SaveAsync(entity, IsCommit) : await UpdateAsync(entity, IsCommit);
        }

        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns></returns>
        public virtual T GetNoTracking<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _Context.Set<T>().AsNoTracking().SingleOrDefault(predicate);
        }
        /// <summary>
        /// 通过Lamda表达式获取实体（异步方式）
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns></returns>
        public virtual async Task<T> GetNoTrackingAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await Task.Run(() => _Context.Set<T>().AsNoTracking().SingleOrDefault(predicate));
        }
        /// <summary>
        /// 通过Lamda表达式获取实体
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns></returns>
        public virtual T GetByTracking<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _Context.Set<T>().SingleOrDefault(predicate);
        }
        /// <summary>
        /// 通过Lamda表达式获取实体（异步方式）
        /// </summary>
        /// <param name="predicate">Lamda表达式（p=>p.Id==Id）</param>
        /// <returns></returns>
        public virtual async Task<T> GetByTrackingAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await Task.Run(() => _Context.Set<T>().SingleOrDefault(predicate));
        }
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool Delete<T>(T entity, bool IsCommit = true) where T : class
        {
            if (entity == null) return false;
            _Context.Set<T>().Attach(entity);
            _Context.Set<T>().Remove(entity);

            if (IsCommit)
                try
                {
                    return _Context.SaveChanges() > 0;
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    return _Context.SaveChanges() > 0;

                }
            else
                return false;
        }
        /// <summary>
        /// 删除一条记录（异步方式）
        /// </summary>
        /// <param name="entity">实体模型</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync<T>(T entity, bool IsCommit = true) where T : class
        {
            if (entity == null) return await Task.Run(() => false);
            _Context.Set<T>().Attach(entity);
            _Context.Set<T>().Remove(entity);
            if (IsCommit)
                try
                {
                    return await Task.Run(() => _Context.SaveChanges() > 0);
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    return await Task.Run(() => _Context.SaveChanges() > 0);

                }
            else
                return await Task.Run(() => false); ;
        }
        #region 多模型 操作

        /// <summary>
        /// 增加多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool SaveList<T>(List<T> T1, bool IsCommit = true) where T : class
        {

            if (T1 == null || T1.Count == 0) return false;

            T1.ToList().ForEach(item =>
            {
                _Context.Set<T>().Add(item);
            });

            if (IsCommit)
                try
                {
                    return _Context.SaveChanges() > 0;
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    return _Context.SaveChanges() > 0;

                }
            else
                return false;
        }
        /// <summary>
        /// 增加多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> SaveListAsync<T>(List<T> T1, bool IsCommit = true) where T : class
        {
            if (T1 == null || T1.Count == 0) return await Task.Run(() => false);

            T1.ToList().ForEach(item =>
            {
                _Context.Set<T>().Add(item);
            });

            if (IsCommit)
                try
                {
                    return await Task.Run(() => _Context.SaveChanges() > 0);
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    return await Task.Run(() => _Context.SaveChanges() > 0);
                }
            else
                return await Task.Run(() => false);
        }




        /// <summary>
        /// 更新多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool UpdateList<T>(List<T> T1, bool IsCommit = true) where T : class
        {
            if (T1 == null || T1.Count == 0) return false;

            T1.ToList().ForEach(item =>
            {
                _Context.Set<T>().Attach(item);
                _Context.Entry<T>(item).State = EntityState.Modified;
            });

            if (IsCommit)
                try
                {
                    return _Context.SaveChanges() > 0;
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    return _Context.SaveChanges() > 0;

                }
            else
                return false;
        }
        /// <summary>
        /// 更新多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateListAsync<T>(List<T> T1, bool IsCommit = true) where T : class
        {
            if (T1 == null || T1.Count == 0) return await Task.Run(() => false);

            T1.ToList().ForEach(item =>
            {
                _Context.Set<T>().Attach(item);
                _Context.Entry<T>(item).State = EntityState.Modified;
            });

            if (IsCommit)
                try
                {
                    return await Task.Run(() => _Context.SaveChanges() > 0);
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    return await Task.Run(() => _Context.SaveChanges() > 0);
                }
            else
                return await Task.Run(() => false);
        }




        /// <summary>
        /// 删除多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool DeleteList<T>(List<T> T1, bool IsCommit = true) where T : class
        {
            if (T1 == null || T1.Count == 0) return false;

            T1.ToList().ForEach(item =>
            {
                _Context.Set<T>().Attach(item);
                _Context.Set<T>().Remove(item);
            });

            if (IsCommit)
                try
                {
                    return _Context.SaveChanges() > 0;
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    return _Context.SaveChanges() > 0;

                }
            else
                return false;
        }
        /// <summary>
        /// 删除多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteListAsync<T>(List<T> T1, bool IsCommit = true) where T : class
        {
            if (T1 == null || T1.Count == 0) return await Task.Run(() => false);

            T1.ToList().ForEach(item =>
            {
                _Context.Set<T>().Attach(item);
                _Context.Set<T>().Remove(item);
            });

            if (IsCommit)
                try
                {
                    return await Task.Run(() => _Context.SaveChanges() > 0);
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    return await Task.Run(() => _Context.SaveChanges() > 0);
                }
            else
                return await Task.Run(() => false);
        }




        /// <summary>
        /// 通过Lamda表达式，删除一条或多条记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public virtual bool Delete<T>(Expression<Func<T, bool>> predicate, bool IsCommit = true) where T : class
        {
            IQueryable<T> entry = (predicate == null) ? _Context.Set<T>().AsQueryable() : _Context.Set<T>().Where(predicate);
            List<T> list = entry.ToList();

            if (list != null && list.Count == 0) return false;
            list.ForEach(item => {
                _Context.Set<T>().Attach(item);
                _Context.Set<T>().Remove(item);
            });

            if (IsCommit)
                try
                {
                    return _Context.SaveChanges() > 0;
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entrynew = e.Entries.Single();
                    entrynew.OriginalValues.SetValues(entrynew.GetDatabaseValues());
                    return _Context.SaveChanges() > 0;

                }
            else
                return false;
        }
        /// <summary>
        /// 通过Lamda表达式，删除一条或多条记录（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> predicate, bool IsCommit = true) where T : class
        {
            IQueryable<T> entry = (predicate == null) ? _Context.Set<T>().AsQueryable() : _Context.Set<T>().Where(predicate);
            List<T> list = entry.ToList();

            if (list != null && list.Count == 0) return await Task.Run(() => false);
            list.ForEach(item => {
                _Context.Set<T>().Attach(item);
                _Context.Set<T>().Remove(item);
            });

            if (IsCommit)
                try
                {
                    return await Task.Run(() => _Context.SaveChanges() > 0);
                }
                catch (DbUpdateConcurrencyException e)//（OptimisticConcurrencyException）
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entrynew = e.Entries.Single();
                    entrynew.OriginalValues.SetValues(entrynew.GetDatabaseValues());
                    return await Task.Run(() => _Context.SaveChanges() > 0);
                }
            else
                return await Task.Run(() => false);
        }

        #endregion
        #endregion

        #region 获取多条数据操作

        /// <summary>
        /// Lamda返回IQueryable集合，延时加载数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> LoadAll<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return predicate != null ? _Context.Set<T>().Where(predicate).AsNoTracking<T>() : _Context.Set<T>().AsQueryable<T>().AsNoTracking<T>();
        }
        /// <summary>
        /// 返回IQueryable集合，延时加载数据（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<IQueryable<T>> LoadAllAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return predicate != null ? await Task.Run(() => _Context.Set<T>().Where(predicate).AsNoTracking<T>()) : await Task.Run(() => _Context.Set<T>().AsQueryable<T>().AsNoTracking<T>());
        }

        /// <summary>
        /// 返回List<T>集合,不采用延时加载
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual List<T> LoadListAll<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return predicate != null ? _Context.Set<T>().Where(predicate).AsNoTracking().ToList() : _Context.Set<T>().AsQueryable<T>().AsNoTracking().ToList();
        }
        // <summary>
        /// 返回List<T>集合,不采用延时加载（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> LoadListAllAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return predicate != null ? await Task.Run(() => _Context.Set<T>().Where(predicate).AsNoTracking().ToList()) : await Task.Run(() => _Context.Set<T>().AsQueryable<T>().AsNoTracking().ToList());
        }

        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual IQueryable<T> LoadAllBySql<T>(string sql, params DbParameter[] para) where T : class
        {
            return _Context.Set<T>().FromSql(sql, para);
        }
        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual async Task<IQueryable<T>> LoadAllBySqlAsync<T>(string sql, params DbParameter[] para) where T : class
        {
            return await Task.Run(() => _Context.Set<T>().FromSql(sql, para));
        }


        /// <summary>
        /// T-Sql方式：返回List<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual List<T> LoadListAllBySql<T>(string sql, params DbParameter[] para) where T : class
        {
            return _Context.Set<T>().FromSql(sql, para).Cast<T>().ToList();
        }
        /// <summary>
        /// T-Sql方式：返回List<T>集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual async Task<List<T>> LoadListAllBySqlAsync<T>(string sql, params DbParameter[] para) where T : class
        {
            return await Task.Run(() => _Context.Set<T>().FromSql(sql, para).Cast<T>().ToList());
        }

        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回实体对象集合
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <typeparam name="TResult">数据结果，与TEntity一致</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>实体集合</returns>
        public virtual List<TResult> QueryEntity<TEntity, TOrderBy, TResult>
            (Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Expression<Func<TEntity, TResult>> selector,
            bool IsAsc)
            where TEntity : class
            where TResult : class
        {
            IQueryable<TEntity> query = _Context.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = IsAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (selector == null)
            {
                return query.Cast<TResult>().AsNoTracking().ToList();
            }
            return query.Select(selector).AsNoTracking().ToList();
        }
        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回实体对象集合（异步方式）
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <typeparam name="TResult">数据结果，与TEntity一致</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>实体集合</returns>
        public virtual async Task<List<TResult>> QueryEntityAsync<TEntity, TOrderBy, TResult>
            (Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Expression<Func<TEntity, TResult>> selector,
            bool IsAsc)
            where TEntity : class
            where TResult : class
        {
            IQueryable<TEntity> query = _Context.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = IsAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (selector == null)
            {
                return await Task.Run(() => query.Cast<TResult>().AsNoTracking().ToList());
            }
            return await Task.Run(() => query.Select(selector).AsNoTracking().ToList());
        }

        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回Object对象集合
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>自定义实体集合</returns>
        public virtual List<object> QueryObject<TEntity, TOrderBy>
            (Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Func<IQueryable<TEntity>,
            List<object>> selector,
            bool IsAsc)
            where TEntity : class
        {
            IQueryable<TEntity> query = _Context.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = IsAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (selector == null)
            {
                return query.AsNoTracking().ToList<object>();
            }
            return selector(query);
        }
        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回Object对象集合（异步方式）
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>自定义实体集合</returns>
        public virtual async Task<List<object>> QueryObjectAsync<TEntity, TOrderBy>
            (Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Func<IQueryable<TEntity>,
            List<object>> selector,
            bool IsAsc)
            where TEntity : class
        {
            IQueryable<TEntity> query = _Context.Set<TEntity>();
            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = IsAsc ? query.OrderBy(orderby) : query.OrderByDescending(orderby);
            }
            if (selector == null)
            {
                return await Task.Run(() => query.AsNoTracking().ToList<object>());
            }
            return await Task.Run(() => selector(query));
        }


        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回动态类对象集合
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>动态类</returns>
        public virtual dynamic QueryDynamic<TEntity, TOrderBy>
            (Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Func<IQueryable<TEntity>,
            List<object>> selector,
            bool IsAsc)
            where TEntity : class
        {
            List<object> list = QueryObject<TEntity, TOrderBy>
                 (where, orderby, selector, IsAsc);
            return JsonConvert.SerializeObject(list); //  Common.JsonHelper.JsonConvert.JsonClass(list);
        }
        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回动态类对象集合（异步方式）
        /// </summary>
        /// <typeparam name="TEntity">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>动态类</returns>
        public virtual async Task<dynamic> QueryDynamicAsync<TEntity, TOrderBy>
            (Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, TOrderBy>> orderby,
            Func<IQueryable<TEntity>,
            List<object>> selector,
            bool IsAsc)
            where TEntity : class
        {
            List<object> list = QueryObject<TEntity, TOrderBy>
                 (where, orderby, selector, IsAsc);
            return await Task.Run(() => JsonConvert.SerializeObject(list));
        }

        #endregion
        #region 验证是否存在

        /// <summary>
        /// 验证当前条件是否存在相同项
        /// </summary>
        public virtual bool IsExist<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var entry = _Context.Set<T>().Where(predicate);
            return (entry.Any());
        }
        /// <summary>
        /// 验证当前条件是否存在相同项（异步方式）
        /// </summary>
        public virtual async Task<bool> IsExistAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var entry = _Context.Set<T>().Where(predicate);
            return await Task.Run(() => entry.Any());
        }

        /// <summary>
        /// 根据SQL验证实体对象是否存在
        /// </summary>
        public virtual bool IsExist(string sql, params DbParameter[] para)
        {
            return _Context.Database.ExecuteSqlCommand(sql, para) > 0;
        }
        /// <summary>
        /// 根据SQL验证实体对象是否存在（异步方式）
        /// </summary>
        public virtual async Task<bool> IsExistAsync(string sql, params DbParameter[] para)
        {
            return await Task.Run(() => _Context.Database.ExecuteSqlCommand(sql, para) > 0);
        }

        #endregion
      
        #region 扩展操作
        public void BulkInsert<TTable>(IEnumerable<TTable> listEntity) where TTable : class
        {
            if (_Context.Database.GetDbConnection().State != ConnectionState.Open)
            {
                _Context.Database.GetDbConnection().Open(); //打开Connection连接  
            }
            BulkInsert<TTable>((SqlConnection)_Context.Database.GetDbConnection(), typeof(TTable).Name, listEntity.ToList());
            if (_Context.Database.GetDbConnection().State != ConnectionState.Closed)
            {
                _Context.Database.GetDbConnection().Close(); //关闭Connection连接  
            }
        
        }
        public void BulkInsertExpand<TTable>(IEnumerable<TTable> listEntity) where TTable : class
        {
            _Context.BulkInsert(listEntity.ToList());
          
        }
        private void BulkInsert<T>(SqlConnection conn, string tableName, IList<T> list)
        {
            using (var bulkCopy = new SqlBulkCopy(conn))
            {
                bulkCopy.BatchSize = list.Count;
                bulkCopy.DestinationTableName = tableName;

                var table = new DataTable();
                var props = TypeDescriptor.GetProperties(typeof(T))

                    .Cast<PropertyDescriptor>()
                    .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
                    .ToArray();

                foreach (var propertyInfo in props)
                {
                    bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                    table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                }

                var values = new object[props.Length];
                foreach (var item in list)
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }

                    table.Rows.Add(values);
                }

                bulkCopy.WriteToServer(table);
            }
        }
        #endregion
        #region 事务处理
        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {

            trans = _Context.Database.BeginTransaction();
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
