using Dapper;
using Dapper.Contrib.Extensions;
using SAAS.DB.Dapper.Repository;
using SAAS.FrameWork.Util.ConfigurationManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAAS.DB.Dapper.Database.DbRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class,new()
    {
        static string ConnectionString = ConfigurationManager.Instance.GetByPath<string>("ConnectionStrings.Order_Db");
        private IDbConnection _dbConnection;
        private IDbTransaction trans;
        /// <summary>
        /// 对象的表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 主键属性对象
        /// </summary>
        public string PrimaryKey { get; set; }

       
        public BaseRepository()
        {

            this.TableName = EntityHelper.GetTableName(typeof(T));
            this.PrimaryKey = EntityHelper.GetPrimaryKey<T>();
            _dbConnection = DbHelp.GetConnection(ProviderType.MySql, ConnectionString);
        }
        #region Dapper原生传递SQL
        public async Task Delete(int Id, string deleteSql)
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                await dbConnection.ExecuteAsync(deleteSql, new { Id = Id });
            }
        }

        public async Task<T> GetOne(int Id, string selectOneSql)
        {

            using (IDbConnection dbConnection = _dbConnection)
            {
               
                var result = await dbConnection.QueryFirstOrDefaultAsync<T>(selectOneSql, new { Id = Id });
                return result;
            }
        }
        public async Task<List<T>> GetList(int Id, string selectSql)
        {

            using (IDbConnection dbConnection = _dbConnection)
            {
               
                return await Task.Run(() => dbConnection.Query<T>(selectSql, new { Id = Id }).ToList());
            }
        }
        public async Task Insert(T entity, string insertSql)
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                await dbConnection.QueryFirstOrDefaultAsync<T>(insertSql, entity);
            }
        }

        public async Task<List<T>> SelectAsync(string selectSql)
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                return await Task.Run(() => dbConnection.Query<T>(selectSql).ToList());
            }
        }

        public async Task Update(T entity, string updateSql)
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                await dbConnection.ExecuteAsync(updateSql, entity);
            }
        }

        public async Task<List<T>> GetByDynamicParams(string sqlText, DynamicParameters dynamicParams)
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                return await Task.Run(() => dbConnection.Query<T>(sqlText, dynamicParams).ToList());
            }
        }
       
        #endregion
        #region dapper contrib
        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                return dbConnection.GetAll<T>();
            }
        }
       
        /// <summary>
        /// 查询数据库,返回指定ID的对象
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <returns></returns>
        public T GetByID(object id)
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                return dbConnection.Get<T>(id);
            }
        }

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public bool Insert(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                dbConnection.Insert(info);
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public bool Insert(IEnumerable<T> list)
        {
            bool result = false;
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                result = dbConnection.Insert(list) > 0;
            }
            return result;
        }

        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public bool Update(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                result = dbConnection.Update(info);
            }
            return result;
        }
        /// <summary>
        /// 更新指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public bool Update(IEnumerable<T> list)
        {
            bool result = false;
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                result = dbConnection.Update(list);
            }
            return result;
        }
        /// <summary>
        /// 从数据库中删除指定对象
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public bool Delete(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                result = dbConnection.Delete(info);
            }
            return result;
        }
        /// <summary>
        /// 从数据库中删除指定对象集合
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public bool Delete(IEnumerable<T> list)
        {
            bool result = false;
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                result = dbConnection.Delete(list);
            }
            return result;
        }
        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns></returns>
        public bool Delete(object id)
        {
            bool result = false;
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                string query = string.Format("DELETE FROM {0} WHERE {1} = @id", TableName, PrimaryKey);
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                result = dbConnection.Execute(query, parameters) > 0;

            }
            return result;
        }
        /// <summary>
        /// 从数据库中删除所有对象
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll()
        {
            bool result = false;
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                result = dbConnection.DeleteAll<T>();
            }
            return result;
        }
        #endregion
        #region dapper contrib async
        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                return await dbConnection.GetAllAsync<T>();
            }
        }

        /// <summary>
        /// 查询数据库,返回指定ID的对象
        /// </summary>
        /// <param name="id">主键的值</param>
        /// <returns></returns>
        public virtual async Task<T> GetByIDAsync(object id)
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                return await dbConnection.GetAsync<T>(id);
            }
        }
        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public virtual async Task<bool> InsertAsync(T info)
        {
            bool result = false;
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                await dbConnection.InsertAsync(info);
                result = true;
            }
            return await Task<bool>.FromResult(result);
        }

        /// <summary>
        /// 插入指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public virtual async Task<bool> InsertAsync(IEnumerable<T> list)
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                return await dbConnection.InsertAsync(list) > 0;
            }
        }
        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(T info)
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                return await dbConnection.UpdateAsync(info);
            }
        }

        /// <summary>
        /// 更新指定对象集合到数据库中
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(IEnumerable<T> list)
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                return await dbConnection.UpdateAsync(list);
            }
        }

        /// <summary>
        /// 从数据库中删除指定对象
        /// </summary>
        /// <param name="info">指定的对象</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(T info)
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                return await dbConnection.DeleteAsync(info);
            }
        }

        /// <summary>
        /// 从数据库中删除指定对象集合
        /// </summary>
        /// <param name="list">指定的对象集合</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(IEnumerable<T> list)
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                return await dbConnection.DeleteAsync(list);
            }
        }

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="id">对象的ID</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(object id)
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                string query = string.Format("DELETE FROM {0} WHERE {1} = @id", TableName, PrimaryKey);
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                return await dbConnection.ExecuteAsync(query, parameters) > 0;
            }
        }

        /// <summary>
        /// 从数据库中删除所有对象
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAllAsync()
        {
            using (IDbConnection dbConnection = _dbConnection)
            {
               
                return await dbConnection.DeleteAllAsync<T>();
            }
        }
        #endregion
        #region 事务
        /// <summary>
        /// 开始一个事务
        /// </summary>
        public void BeginTransaction()
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();

             trans = _dbConnection.BeginTransaction();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            trans.Commit();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollBackTransaction()
        {
            trans.Rollback();
        }
        #endregion
    }

}
