using Microsoft.EntityFrameworkCore;
using SAAS.FrameWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SAAS.Framework.Orm.EfCore.UnitWork
{
    public interface IRepository<TDbContext, TEntity>  where TDbContext : DbContext
    {
        TDbContext DBContext { get; }
        /// <summary>
        /// 根据主键获取单个对象
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetByKey<TKey>(TKey id);
        /// <summary>
        /// 异步根据主键获取单个对象
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByKeyAsync<TKey>(TKey id);
        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IList<TEntity> Get(
          Expression<Func<TEntity, bool>> predicate = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          List<Expression<Func<TEntity, object>>> includes = null);
        /// <summary>
        /// 异步获取对象集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null);
        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        /// <summary>
        /// 增加多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        bool AddList<T>(List<T> T1) where T : class;
        /// <summary>
        /// 增加多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <returns></returns>
        Task<bool> AddListAsync<T>(List<T> T1) where T : class;
        /// <summary>
        /// 跟新对象
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        TEntity Update(TEntity entity);
        /// <summary>
        /// 更新多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <returns></returns>
        bool UpdateList<T>(List<T> T1) where T : class;
        /// <summary>
        /// 更新多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <returns></returns>
        Task<bool> UpdateListAsync<T>(List<T> T1) where T : class;
       /// <summary>
       /// 根据主键ID删除对象
       /// </summary>
       /// <typeparam name="TKey"></typeparam>
       /// <param name="id"></param>
        void Delete<TKey>(TKey id);
        /// <summary>
        /// 根据对象删除对象
        /// </summary>
        /// <param name="entity"></param>

        void Delete(TEntity entity);
        /// <summary>
        /// 删除多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <returns></returns>
        bool DeleteList<T>(List<T> T1) where T : class;
        /// <summary>
        /// 删除多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <returns></returns>
        Task<bool> DeleteListAsync<T>(List<T> T1) where T : class;
        /// <summary>
        /// 通过Lamda表达式，删除一条或多条记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Delete<T>(Expression<Func<T, bool>> predicate) where T : class;
        /// <summary>
        /// 通过Lamda表达式，删除一条或多条记录（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        //查询分页
        Task<List<TEntity>> GetList<s>(int pageIndex, int pageSize, System.Linq.Expressions.Expression<Func<TEntity, bool>> whereLambda, System.Linq.Expressions.Expression<Func<TEntity, s>> orderbyLambda, bool isAsc);

        //获取总条数
        Task<int> GetTotalCount(Expression<Func<TEntity, bool>> whereLambda);
        #region 表达式的操作

        /// <summary>
        /// 返回IQueryable集合，延时加载数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> LoadAll<T>(Expression<Func<T, bool>> predicate) where T : class;
        /// <summary>
        /// 返回IQueryable集合，延时加载数据（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IQueryable<T>> LoadAllAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        // <summary>
        /// 返回List<T>集合,不采用延时加载
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<T> LoadListAll<T>(Expression<Func<T, bool>> predicate) where T : class;
        // <summary>
        /// 返回List<T>集合,不采用延时加载（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<T>> LoadListAllAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        IQueryable<T> LoadAllBySql<T>(string sql, params SqlParameter[] para) where T : class;
        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        IQueryable<T> LoadAllBySql<T>(string sql, params object[] parameters) where T : class;
        /// <summary>
        /// T-Sql方式：返回IQueryable<T>集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        Task<IQueryable<T>> LoadAllBySqlAsync<T>(string sql, params SqlParameter[] para) where T : class;

        /// <summary>
        /// T-Sql方式：返回List<T>集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        List<T> LoadListAllBySql<T>(string sql, params SqlParameter[] para) where T : class;
        /// <summary>
        /// T-Sql方式：返回List<T>集合（异步方式）
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="para">Parameters参数</param>
        /// <returns></returns>
        Task<List<T>> LoadListAllBySqlAsync<T>(string sql, params SqlParameter[] para) where T : class;

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
        List<TResult> QueryEntity<T, TOrderBy, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderby, Expression<Func<T, TResult>> selector, bool IsAsc)
            where T : class
            where TResult : class;
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
        Task<List<TResult>> QueryEntityAsync<T, TOrderBy, TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderby, Expression<Func<T, TResult>> selector, bool IsAsc)
            where T : class
            where TResult : class;

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
        List<object> QueryObject<T, TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderby, Func<IQueryable<T>, List<object>> selector, bool IsAsc)
            where T : class;
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
        Task<List<object>> QueryObjectAsync<T, TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderby, Func<IQueryable<T>, List<object>> selector, bool IsAsc)
            where T : class;

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
        dynamic QueryDynamic<T, TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderby, Func<IQueryable<T>, List<object>> selector, bool IsAsc)
            where T : class;
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
        Task<dynamic> QueryDynamicAsync<T, TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderby, Func<IQueryable<T>, List<object>> selector, bool IsAsc)
            where T : class;
        #region 验证是否存在

        /// <summary>
        /// 验证当前条件是否存在相同项
        /// </summary>
        bool IsExist<T>(Expression<Func<T, bool>> predicate) where T : class;
        /// <summary>
        /// 验证当前条件是否存在相同项（异步方式）
        /// </summary>
        Task<bool> IsExistAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        /// <summary>
        /// 根据SQL验证实体对象是否存在
        /// </summary>
        bool IsExist(string sql, params SqlParameter[] para);
        /// <summary>
        /// 根据SQL验证实体对象是否存在（异步方式）
        /// </summary>
        Task<bool> IsExistAsync(string sql, params SqlParameter[] para);

        #endregion
        #endregion
        #region  扩展操作-批量操作
        /// <summary>
        /// 批量导入使用原生
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <param name="listEntity"></param>
        void BulkInsert<TTable>(IEnumerable<TTable> listEntity) where TTable : class;
        /// <summary>
        /// 批量导入使用使用扩展
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <param name="listEntity"></param>
        void BulkInsertExpand<TTable>(IEnumerable<TTable> listEntity) where TTable : class;
        #endregion
        #region SQL语句

        int ExecuteSql(string sql);

        Task<int> ExecuteSqlAsync(string sql);

        int ExecuteSql(string sql, List<SqlParameter> spList);
        int ExecuteSql(string sql, params object[] parameters);

        Task<int> ExecuteSqlAsync(string sql, List<SqlParameter> spList);



        DataTable GetDataTableWithSql(string sql);


        DataTable GetDataTableWithSql(string sql, List<SqlParameter> spList);


        #endregion

        ApiResponsePage<TTable> ToPage<TTable>(ApiRequestPage<IQueryable<TTable>> request) where TTable : class;
        /// <summary>
        /// 执行sql命令的分页集合，采用2012最新的方式
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <param name="header"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        ApiResponsePage<TTable> ToPage<TTable>(ApiRequestPage header, string sql, object[] parameters) where TTable : class, new();
        /// <summary>
        /// 执行sql命令的分页集合，采用rownumer
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <param name="header"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        ApiResponsePage<TTable> ToPageRowNumber<TTable>(ApiRequestPage header, string sql, object[] parameters) where TTable : class, new();
    }
}
