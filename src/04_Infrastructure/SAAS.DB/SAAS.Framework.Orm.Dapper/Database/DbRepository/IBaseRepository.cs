using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SAAS.DB.Dapper.Database.DbRepository
{
    public interface IBaseRepository<T> where T : class,new()
    {
        #region Dapper原生传递sql
        Task Insert(T entity, string insertSql);

        Task Update(T entity, string updateSql);

        Task Delete(int Id, string deleteSql);

        Task<List<T>> SelectAsync(string selectSql);

        Task<T> GetOne(int Id, string selectOneSql);

        Task<List<T>> GetList(int Id, string selectSql);

        Task<List<T>> GetByDynamicParams(string sqlText, DynamicParameters dynamicParams);
       
        #endregion
        #region Dapper contrib
        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        

        /// <summary>
        /// 查询数据库,返回指定ID的对象
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <returns></returns>
        T GetByID(object id);
        

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        bool Insert(T info);
        
        /// <summary>
        /// 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        bool Insert(IEnumerable<T> list);
        

        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        bool Update(T info);
       
        /// <summary>
        /// 更新指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        bool Update(IEnumerable<T> list);
        
        /// <summary>
        /// 从数据库中删除指定对象
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
         bool Delete(T info);
        
        /// <summary>
        /// 从数据库中删除指定对象集合
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        bool Delete(IEnumerable<T> list);
       
        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns></returns>
         bool Delete(object id);
       
        /// <summary>
        /// 从数据库中删除所有对象
        /// </summary>
        /// <returns></returns>
         bool DeleteAll();
        #endregion
        #region Dapper contrib
        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();
        
        /// <summary>
        /// 查询数据库,返回指定ID的对象
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <returns></returns>
        Task<T> GetByIDAsync(object id);
        
        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        Task<bool> InsertAsync(T info);
       

        /// <summary>
        /// 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        Task<bool> InsertAsync(IEnumerable<T> list);
        
        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T info);
       
        /// <summary>
        /// 更新指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(IEnumerable<T> list);
        
        /// <summary>
        /// 从数据库中删除指定对象
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T info);
       
        /// <summary>
        /// 从数据库中删除指定对象集合
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<T> list);
        
        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(object id);
        /// <summary>
        /// 从数据库中删除所有对象
        /// </summary>
        /// <returns></returns>
          Task<bool> DeleteAllAsync();

        #endregion
        #region 事务
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        #endregion
    }

}
