using App.AuthCenter.Logical.Entity;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using SAAS.DB.Dapper;
using SAAS.DB.Dapper.Database;
using SAAS.DB.Dapper.Database.DbRepository;
using SAAS.FrameWork.Util.ConfigurationManager;
using System;
using System.Collections.Generic;
namespace Demo.Domain
{
    /// <summary>
    /// 领域层：做元数据的仓储操作，元数据的验证
    /// </summary>
    public class DemoMomainService
    {
       // readonly BaseRepository<AccountData> _account= new BaseRepository<AccountData>();
        static string ConnectionString = ConfigurationManager.Instance.GetByPath<string>("ConnectionStrings.Order_Db");
      
        public DemoMomainService()
        {
           


        }

        public string GetStrTest()
        {
            using (var conn = DbHelp.GetConnection(ProviderType.MySql, ConnectionString))
            {
                var obj= conn.Get<AccountData>(1);

            }
           // var objmodel=_account.GetByID(1);
            return "成功返回字符串！";
        }

       
    }
}
