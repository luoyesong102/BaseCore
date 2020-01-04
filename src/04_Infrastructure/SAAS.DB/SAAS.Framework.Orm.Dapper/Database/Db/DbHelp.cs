using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Microsoft.Data.Sqlite;

namespace SAAS.DB.Dapper.Database
{
    public class DbHelp
    {
        //static string ConnectionString = ConfigurationManager.Instance.GetByPath<string>("ConnectionStrings.Order_Db");
        //https://github.com/SkyChenSky/Sikiro.Dapper.Extension  扩展lamada表达式及DB生成Model工具
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ConnectionString"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DbConnection GetConnection(ProviderType type, string ConnectionString, string filePath=null)
        {
            DbConnection conn=null;
            switch (type)
            {
                case ProviderType.SqlServer:
                    conn = new SqlConnection(ConnectionString);
                    break;
                case ProviderType.MySql:
                    conn = new MySqlConnection(ConnectionString);
                    break;
                case ProviderType.Sqlite:
                    var connectionStringBuilder = new SqliteConnectionStringBuilder();
                    connectionStringBuilder.DataSource = filePath;
                    conn= new SqliteConnection(connectionStringBuilder.ConnectionString);
                    break;
            }
            conn.Open();
            return conn;
        }
    }
}
