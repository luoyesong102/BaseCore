using App.AuthCenter.Logical.Entity;
using Dapper;
using Dapper.Contrib.Extensions;
using SAAS.DB.Dapper;
using SAAS.DB.Dapper.Database;
using SAAS.FrameWork.Log;
using SAAS.FrameWork.Module.Api.Data;
using SAAS.FrameWork.Module.Sql.List;
using SAAS.FrameWork.Module.Sql.Mysql;
using SAAS.FrameWork.Util.ConfigurationManager;
using SAAS.FrameWork.Util.SsError;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Domain
{
    /// <summary>
    /// 领域层：做元数据的仓储操作，元数据的验证
    /// </summary>
    public class AccountMomainService
    {
        static string ConnectionString = ConfigurationManager.Instance.GetByPath<string>("ConnectionStrings.Order_Db");
        public AccountMomainService()
        {
        }
        #region 查询用户类

      
        /// <summary>
        /// 主键获取唯一对象
        /// </summary>
        /// <param name="decorator_flow_id"></param>
        /// <returns></returns>
        public  AccountData GetUserInfo(long userId)
        {
            using (var conn = DbHelp.GetConnection(ProviderType.MySql, ConnectionString))
            {
                return conn.Get<AccountData>(userId);

            }
        }
        /// <summary>
        /// 根据用户名密码查询用户对象
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public AccountData FindUser(string account, string pwd = null)
        {
            #region 查数据库            
            using (var conn = DbHelp.GetConnection(ProviderType.MySql, ConnectionString))
            {
                if (null == pwd)
                {
                    return conn.QueryFirstOrDefault<AccountData>("select * from tb_account where account=@account", new { account });
                }
                else
                {
                    return conn.QueryFirstOrDefault<AccountData>("select * from tb_account where account=@account and pwd=@pwd", new { account, pwd });
                }
            }
            #endregion
        }
        /// <summary>
        /// 根据角色获取用户对象
        /// </summary>
        /// <param name="order_flow_id"></param>
        /// <returns></returns>
        public  List<AccountData> GetRoleUserList(string role)
        {

            using (var conn = DbHelp.GetConnection(ProviderType.MySql, ConnectionString))
            {
                return conn.Query<AccountData>($"select * from {AccountData.tableName} where role=@role  ", new { role }).ToList();

            }

        }
        /// <summary>
        /// 根据条件获取分页用户信息
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="filter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public  ApiReturn<PageData<AccountData>> GetPageUserList(PageInfo pager, DataFilter[] filter, SortItem[] sort)
        {

            var apiRet = new ApiReturn<PageData<AccountData>>();
            using (var conn = DbHelp.GetConnection(ProviderType.MySql, ConnectionString))
            {
                var pageData = new PageData<AccountData>(pager);

                #region 动态生成条件
                //var param = new DynamicParameters();

                //var sqlBuild = new SqlBuild
                //{
                //    pager = pager,
                //    filter = filter,
                //    sort = sort,
                //    addSqlParam = (paramName, paramValue) => { param.Add(paramName, paramValue); }
                //}.Build();
                var sqlBuild = new DapperSqlBuild
                {
                    pager = pager,
                    filter = filter,
                    sort = sort
                };
                sqlBuild.Build();
                #endregion
                var sqlWhere = sqlBuild.sqlWhere;
                var sqlOrderBy = sqlBuild.sqlOrderBy;
                var sqlLimit = sqlBuild.sqlLimit;
                sqlOrderBy = " order by " + (sqlOrderBy ?? "a.id desc");
                sqlWhere += "  and account_state =1" ;
                var sqlList = $"select  * from  {AccountData.tableName} where 1=1 " + sqlWhere;
                pageData.totalCount = conn.ExecuteScalar<int>($"select count(1) from ({sqlList}) tb ", sqlBuild.sqlParam);
                var sql = sqlList + sqlOrderBy + sqlLimit;
                var rows = pageData.rows = conn.Query<AccountData>(sql, sqlBuild.sqlParam).ToList();
                apiRet.data = pageData;
            }
            return apiRet;

        }

        /// <summary>
        /// 验证当前是否有正在审核的款项
        /// </summary>
        /// <param name="order_flow_id"></param>
        /// <param name="bill_id"></param>
        /// <returns></returns>
        public ApiReturn<int> GetCount(string role)
        {
            using (var conn = DbHelp.GetConnection(ProviderType.MySql, ConnectionString))
            {
               return conn.ExecuteScalar<int>($"select count(1) from {AccountData.tableName}  where  role=@role ", new { role });
            }
        }
        #endregion
        #region 添加用户类

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public  ApiReturn Add(AccountData data)
        {
            using (var conn = DbHelp.GetConnection(ProviderType.MySql, ConnectionString))
            {
                return 0 < conn.Insert(data);
            }
        }

        #endregion
        #region 修改用户类
        /// <summary>
        /// 根据条件修改用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ApiReturn<int> UpdateUser(string name,long userId)
        {
            using (var conn = DbHelp.GetConnection(ProviderType.MySql, ConnectionString))
            {

              return  conn.Execute($"update {AccountData.tableName} set name=@name  where userId=@userId", new { userId = userId, name = name });
              
            }
        }
        /// <summary>
        /// 修改用户信息，先查询后修改
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public  ApiReturn UpdateAccout(AccountData account)
        {
            using (var conn =DbHelp.GetConnection(ProviderType.MySql, ConnectionString))
            {

                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                       
                        conn.Update(account);
                        conn.Update(account);
                      
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        Logger.log.Error(ex);
                        return SsError.Err_SysErr;
                    }

                }
            }
        }
        #endregion
        #region 删除用户类
        /// <summary>
        /// 根据主键ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiReturn DeleteUserByID(long userId)
        {
            using (var conn = DbHelp.GetConnection(ProviderType.MySql, ConnectionString))
            {
                return conn.Delete<AccountData>(new AccountData() { userId = userId });
            }
        }
        /// <summary>
        /// 根据姓名删除
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ApiReturn<int> DeleteUserByName(string name)
        {
            using (var conn = DbHelp.GetConnection(ProviderType.MySql, ConnectionString))
            {
               return  conn.Execute($"delete from  {AccountData.tableName}    where name=@name", new { name = name });
            }
        }
        #endregion
    }
}
