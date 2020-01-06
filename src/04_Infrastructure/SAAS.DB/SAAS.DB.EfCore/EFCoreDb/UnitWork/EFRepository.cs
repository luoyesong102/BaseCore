using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using System.Data.SqlClient;
using System.Data.Common;
using Newtonsoft.Json;
using SAAS.FrameWork;
using SAAS.Framework.Orm.EfCore.DataAccess;
using SAAS.FrameWork.Util.Exp;
using SAAS.FrameWork.Util.Common;
using System.Text.RegularExpressions;

namespace SAAS.Framework.Orm.EfCore.UnitWork
{
    public class EFRepository<TDbContext, TEntity> : IRepository<TDbContext, TEntity> where TEntity : class where TDbContext : DbContext
    {
        protected readonly TDbContext _context;
        protected readonly DbSet<TEntity> dbSet;
        /// <summary>
        /// 当前DBcoext
        /// </summary>
        public TDbContext DBContext
        {
            get { return _context; }

        }

        public EFRepository(TDbContext context)
        {
            this._context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByKeyAsync<TKey>(TKey id)
        {
            return await dbSet.FindAsync(id);
        }
        public virtual TEntity GetByKey<TKey>(TKey id)
        {
            return  dbSet.Find(id);
        }
        public virtual async Task<IList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null)
        {
            IQueryable<TEntity> query = dbSet;

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

            return await query.ToListAsync();
        }
        public virtual IList<TEntity> Get(
          Expression<Func<TEntity, bool>> predicate = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          List<Expression<Func<TEntity, object>>> includes = null)
        {
            IQueryable<TEntity> query = dbSet;

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

            return  query.ToList();
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await dbSet.AddAsync(entity);
            return result.Entity;
        }
        public virtual TEntity Add(TEntity entity)
        {
            var result =  dbSet.Add(entity);
            return result.Entity;
        }
        /// <summary>
        /// 增加多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <returns></returns>
        public virtual bool AddList<T>(List<T> T1) where T : class
        {

            if (T1 == null || T1.Count == 0) return false;

            T1.ToList().ForEach(item =>
            {
                _context.Set<T>().Add(item);
            });
            return true;

        }
        /// <summary>
        /// 增加多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <returns></returns>
        public virtual async Task<bool> AddListAsync<T>(List<T> T1) where T : class
        {
            if (T1 == null || T1.Count == 0) return await Task.Run(() => false);

            T1.ToList().ForEach(item =>
            {
                _context.Set<T>().Add(item);
            });
            return true;
        }


        public virtual TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            this._context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        /// <summary>
        /// 更新多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <returns></returns>
        public virtual bool UpdateList<T>(List<T> T1) where T : class
        {
            if (T1 == null || T1.Count == 0) return false;

            T1.ToList().ForEach(item =>
            {
                _context.Set<T>().Attach(item);
                _context.Entry<T>(item).State = EntityState.Modified;
            });


            return true;
        }
        /// <summary>
        /// 更新多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateListAsync<T>(List<T> T1) where T : class
        {
            if (T1 == null || T1.Count == 0) return await Task.Run(() => false);

            T1.ToList().ForEach(item =>
            {
                _context.Set<T>().Attach(item);
                _context.Entry<T>(item).State = EntityState.Modified;
            });


            return true;
        }
        public virtual void Delete<TKey>(TKey id)
        {
            TEntity entity = dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            dbSet.Remove(entity);
        }

        /// <summary>
        /// 删除多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <returns></returns>
        public virtual bool DeleteList<T>(List<T> T1) where T : class
        {
            if (T1 == null || T1.Count == 0) return false;

            T1.ToList().ForEach(item =>
            {
                _context.Set<T>().Attach(item);
                _context.Set<T>().Remove(item);
            });


            return true;
        }
        /// <summary>
        /// 删除多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteListAsync<T>(List<T> T1) where T : class
        {
            if (T1 == null || T1.Count == 0) return await Task.Run(() => false);

            T1.ToList().ForEach(item =>
            {
                _context.Set<T>().Attach(item);
                _context.Set<T>().Remove(item);
            });
            return true;
        }
        /// <summary>
        /// 查询多条数据-根据传进来的lambda和排序的lambda查询
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetList<s>(int pageIndex, int pageSize,
            System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda,
            System.Linq.Expressions.Expression<Func<TEntity, s>> orderbyLambda, bool isAsc)
        {
            var temp = _context.Set<TEntity>().Where(whereLambda);
            List<TEntity> list = null;
            if (isAsc)//升序
            {
                list = await temp.OrderBy(orderbyLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else//降序
            {
                list = await temp.OrderByDescending(orderbyLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            return list;
        }

        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<int> GetTotalCount(Expression<Func<TEntity, bool>> whereLambda)
        {
            return await _context.Set<TEntity>().Where(whereLambda).CountAsync();
        }




        #region 表达式 查询
        /// <summary>
        /// 通过Lamda表达式，删除一条或多条记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual bool Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            IQueryable<T> entry = (predicate == null) ? _context.Set<T>().AsQueryable() : _context.Set<T>().Where(predicate);
            List<T> list = entry.ToList();

            if (list != null && list.Count == 0) return false;
            list.ForEach(item => {
                _context.Set<T>().Attach(item);
                _context.Set<T>().Remove(item);
            });
            return true;
        }
        /// <summary>
        /// 通过Lamda表达式，删除一条或多条记录（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            IQueryable<T> entry = (predicate == null) ? _context.Set<T>().AsQueryable() : _context.Set<T>().Where(predicate);
            List<T> list = entry.ToList();

            if (list != null && list.Count == 0) return await Task.Run(() => false);
            list.ForEach(item => {
                _context.Set<T>().Attach(item);
                _context.Set<T>().Remove(item);
            });

            return true;
        }
        /// <summary>
        /// Lamda返回IQueryable集合，延时加载数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> LoadAll<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return predicate != null ? _context.Set<T>().Where(predicate).AsNoTracking<T>() : _context.Set<T>().AsQueryable<T>().AsNoTracking<T>();
        }
        /// <summary>
        /// 返回IQueryable集合，延时加载数据（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<IQueryable<T>> LoadAllAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return predicate != null ? await Task.Run(() => _context.Set<T>().Where(predicate).AsNoTracking<T>()) : await Task.Run(() => _context.Set<T>().AsQueryable<T>().AsNoTracking<T>());
        }

        /// <summary>
        /// 返回List<T>集合,不采用延时加载
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual List<T> LoadListAll<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return predicate != null ? _context.Set<T>().Where(predicate).AsNoTracking().ToList() : _context.Set<T>().AsQueryable<T>().AsNoTracking().ToList();
        }
        // <summary>
        /// 返回List<T>集合,不采用延时加载（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> LoadListAllAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return predicate != null ? await Task.Run(() => _context.Set<T>().Where(predicate).AsNoTracking().ToList()) : await Task.Run(() => _context.Set<T>().AsQueryable<T>().AsNoTracking().ToList());
        }

        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual IQueryable<T> LoadAllBySql<T>(string sql, params SqlParameter[] para) where T : class
        {
            return _context.Set<T>().FromSql(sql, para);
        }
        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual IQueryable<T> LoadAllBySql<T>(string sql, params object[] parameters) where T : class
        {
            return _context.Set<T>().FromSql(sql, parameters);
        }
     
        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual async Task<IQueryable<T>> LoadAllBySqlAsync<T>(string sql, params SqlParameter[] para) where T : class
        {
            return await Task.Run(() => _context.Set<T>().FromSql(sql, para));
        }


        /// <summary>
        /// T-Sql方式：返回List<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual List<T> LoadListAllBySql<T>(string sql, params SqlParameter[] para) where T : class
        {
            return _context.Set<T>().FromSql(sql, para).Cast<T>().ToList();
        }
        /// <summary>
        /// T-Sql方式：返回List<T>集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        public virtual async Task<List<T>> LoadListAllBySqlAsync<T>(string sql, params SqlParameter[] para) where T : class
        {
            return await Task.Run(() => _context.Set<T>().FromSql(sql, para).Cast<T>().ToList());
        }

        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回实体对象集合
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <typeparam name="TResult">数据结果，与TEntity一致</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>实体集合</returns>
        public virtual List<TResult> QueryEntity<T, TOrderBy, TResult>
            (Expression<Func<T, bool>> where,
            Expression<Func<T, TOrderBy>> orderby,
            Expression<Func<T, TResult>> selector,
            bool IsAsc)
            where T : class
            where TResult : class
        {
            IQueryable<T> query = _context.Set<T>();
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
        /// <typeparam name="T">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <typeparam name="TResult">数据结果，与TEntity一致</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>实体集合</returns>
        public virtual async Task<List<TResult>> QueryEntityAsync<T, TOrderBy, TResult>
            (Expression<Func<T, bool>> where,
            Expression<Func<T, TOrderBy>> orderby,
            Expression<Func<T, TResult>> selector,
            bool IsAsc)
            where T : class
            where TResult : class
        {
            IQueryable<T> query = _context.Set<T>();
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
        /// <typeparam name="T">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>自定义实体集合</returns>
        public virtual List<object> QueryObject<T, TOrderBy>
            (Expression<Func<T, bool>> where,
            Expression<Func<T, TOrderBy>> orderby,
            Func<IQueryable<T>,
            List<object>> selector,
            bool IsAsc)
            where T : class
        {
            IQueryable<T> query = _context.Set<T>();
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
        /// <typeparam name="T">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>自定义实体集合</returns>
        public virtual async Task<List<object>> QueryObjectAsync<T, TOrderBy>
            (Expression<Func<T, bool>> where,
            Expression<Func<T, TOrderBy>> orderby,
            Func<IQueryable<T>,
            List<object>> selector,
            bool IsAsc)
            where T : class
        {
            IQueryable<T> query = _context.Set<T>();
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
        /// <typeparam name="T">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>动态类</returns>
        public virtual dynamic QueryDynamic<T, TOrderBy>
            (Expression<Func<T, bool>> where,
            Expression<Func<T, TOrderBy>> orderby,
            Func<IQueryable<T>,
            List<object>> selector,
            bool IsAsc)
            where T : class
        {
            List<object> list = QueryObject<T, TOrderBy>
                 (where, orderby, selector, IsAsc);
            return JsonConvert.SerializeObject(list); //  Common.JsonHelper.JsonConvert.JsonClass(list);
        }
        /// <summary>
        /// 可指定返回结果、排序、查询条件的通用查询方法，返回动态类对象集合（异步方式）
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <typeparam name="TOrderBy">排序字段类型</typeparam>
        /// <param name="where">过滤条件，需要用到类型转换的需要提前处理与数据表一致的</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="selector">返回结果（必须是模型中存在的字段）</param>
        /// <param name="IsAsc">排序方向，true为正序false为倒序</param>
        /// <returns>动态类</returns>
        public virtual async Task<dynamic> QueryDynamicAsync<T, TOrderBy>
            (Expression<Func<T, bool>> where,
            Expression<Func<T, TOrderBy>> orderby,
            Func<IQueryable<T>,
            List<object>> selector,
            bool IsAsc)
            where T : class
        {
            List<object> list = QueryObject<T, TOrderBy>
                 (where, orderby, selector, IsAsc);
            return await Task.Run(() => JsonConvert.SerializeObject(list));
        }
        #region 验证是否存在

        /// <summary>
        /// 验证当前条件是否存在相同项
        /// </summary>
        public virtual bool IsExist<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var entry = _context.Set<T>().Where(predicate);
            return (entry.Any());
        }
        /// <summary>
        /// 验证当前条件是否存在相同项（异步方式）
        /// </summary>
        public virtual async Task<bool> IsExistAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var entry = _context.Set<T>().Where(predicate);
            return await Task.Run(() => entry.Any());
        }

        /// <summary>
        /// 根据SQL验证实体对象是否存在
        /// </summary>
        public virtual bool IsExist(string sql, params SqlParameter[] para)
        {
            return _context.Database.ExecuteSqlCommand(sql, para) > 0;
        }
        /// <summary>
        /// 根据SQL验证实体对象是否存在（异步方式）
        /// </summary>
        public virtual async Task<bool> IsExistAsync(string sql, params SqlParameter[] para)
        {
            return await Task.Run(() => _context.Database.ExecuteSqlCommand(sql, para) > 0);
        }

        #endregion
        #endregion

        #region SQL语句

        public int ExecuteSql(string sql)
        {
            return _context.Database.ExecuteSqlCommand(sql);
        }

        public Task<int> ExecuteSqlAsync(string sql)
        {
            return _context.Database.ExecuteSqlCommandAsync(sql);
        }

        public int ExecuteSql(string sql, List<SqlParameter> spList)
        {
            return _context.Database.ExecuteSqlCommand(sql, spList.ToArray());
        }
        public int ExecuteSql(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlCommand(sql, parameters);
        }
        public Task<int> ExecuteSqlAsync(string sql, List<SqlParameter> spList)
        {
            return _context.Database.ExecuteSqlCommandAsync(sql, spList.ToArray());
        }


        public  DataTable GetDataTableWithSql(string sql)
        {
            return GetDataTableWithSql(sql);
        }

        public  DataTable GetDataTableWithSql(string sql, List<SqlParameter> spList)
        {
            DataTable dt = new DataTable(); ;
            using (SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.CommandType = CommandType.Text;
                if (spList.ToArray() != null)
                {
                    da.SelectCommand.Parameters.AddRange(spList.ToArray());
                }
                da.Fill(dt);
            }
            return dt;
        }

        #endregion
        #region 扩展操作
        public void BulkInsert<TTable>(IEnumerable<TTable> listEntity) where TTable : class
        {
            if (_context.Database.GetDbConnection().State != ConnectionState.Open)
            {
                _context.Database.GetDbConnection().Open(); //打开Connection连接  
            }
            BulkInsert<TTable>((SqlConnection)_context.Database.GetDbConnection(), typeof(TTable).Name, listEntity.ToList());
            if (_context.Database.GetDbConnection().State != ConnectionState.Closed)
            {
                _context.Database.GetDbConnection().Close(); //关闭Connection连接  
            }

        }
        public void BulkInsertExpand<TTable>(IEnumerable<TTable> listEntity) where TTable : class
        {
            _context.BulkInsert(listEntity.ToList());

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
        #region 动态条件封装
        public ApiResponsePage<TTable> ToPage<TTable>(ApiRequestPage<IQueryable<TTable>> request) where TTable : class
        {

            var response = new ApiResponsePage<TTable>();

            if (string.IsNullOrEmpty(request.OrderBy))
            {
                throw new BaseException("分页排序字段不能为空");
            }

            response.Count = request.Where.Count();//AsNoTracking().Count();

            response.Body = request.Where.OrderBy(request.OrderBy, request.SortCol== Sort.Desc).Skip<TTable>((request.PageIndex - 1) * request.PageSize).Take<TTable>(request.PageSize).ToList(); //AsNoTracking().ToList();

            return response;

        }
        #endregion
        #region 多表SQL分页
        /// <summary>
        /// sqlserver2012以上分页SQL
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <param name="header"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>

        /// <returns></returns>
        public ApiResponsePage<TTable> ToPage<TTable>(ApiRequestPage header, string sql, params object[] parameters) where TTable : class, new()
        {

            var soaDataPage = new ApiResponsePage<TTable>();
            var fromIndex = SqlUtility.LocationSqlKeyWord(sql, "from");
            var subFromToEnd = sql.Substring(fromIndex);
            string dataCountSql = string.Format("SELECT COUNT(1)  {0} ", subFromToEnd);
            int count = _context.Database.FromSql<listCount>(dataCountSql, parameters.Select(x => ((ICloneable)x).Clone()).ToArray()).FirstOrDefault().Count;
            string pagesql = string.Format("{0} order by {1} {2}   OFFSET {3} ROWS FETCH NEXT {4} ROWS ONLY", sql, header.OrderBy, header.SortCol, (header.PageIndex - 1) * header.PageSize, header.PageIndex * header.PageSize);
            var body = _context.Database.FromSql<TTable>(pagesql, parameters.Select(x => ((ICloneable)x).Clone()).ToArray()).ToArray();
            soaDataPage.Count = count;
            soaDataPage.Body = body;
            return soaDataPage;
        }
        /// <summary>
        /// 统用分页SQL
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <param name="header"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ApiResponsePage<TTable> ToPageRowNumber<TTable>(ApiRequestPage header, string sql, object[] parameters) where TTable : class, new()
        {
            var soaDataPage = new ApiResponsePage<TTable>();
            var fromIndex = SqlUtility.LocationSqlKeyWord(sql, "from");
            var subFromToEnd = sql.Substring(fromIndex);
            string dataCountSql = string.Format("SELECT COUNT(1)  as Count {0} ", subFromToEnd);
            int count = _context.Database.FromSql<listCount>(dataCountSql, parameters.Select(x => ((ICloneable)x).Clone()).ToArray()).FirstOrDefault().Count;
            string rowstr = string.Format("select row_number() over ( order by {0} {1} ) as rownum,", header.OrderBy, header.SortCol);
            sql = sql.ToLower();
            Regex regexobj = new Regex("select");
            sql=regexobj.Replace(sql, rowstr, 1);
            string pagesql = string.Format("select * from ({0}) tt where tt.rownum >{1} and tt.rownum <={2} ", sql, (header.PageIndex - 1) * header.PageSize, header.PageIndex * header.PageSize);
            var body = _context.Database.FromSql<TTable>(pagesql, parameters.Select(x => ((ICloneable)x).Clone()).ToArray()).ToArray();
            soaDataPage.Count = count;
            soaDataPage.Body = body;
            return soaDataPage;
        }
        #endregion
        protected virtual void AttachIfNot(TEntity entity)
        {
            if (this._context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
        }
    }
}
