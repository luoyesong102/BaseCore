using  CacheSqlXmlService;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace CacheSqlXmlService.SqlManager
{
    public static class DataCommandHelper
    {
        static DataCommandHelper()
        {
          
        }
     
      
        private static Dictionary<string, DataCommandConfig> GetAllDataCommandConfigInfosFromCache()
        {
            Dictionary<string, DataCommandConfig> result;
            result = CacheManager.GetWithLocalCache<Dictionary<string, DataCommandConfig>>("DA_GetAllDataCommandConfigInfosFromCache", () =>
            {
                List<string> list;
                var dictionary = GetAllDataCommandConfigInfos(out list);
                CacheFactory.GetInstance().Add("DA_GetAllDataCommandConfigInfosFromCache", dictionary);
                return dictionary;
            });            
            return result;
        }
        private static Dictionary<string, DataCommandConfig> GetAllDataCommandConfigInfos(out List<string> configFileList)
        {
            DataCommandFileList dataCommandFileList = ConfigHelper.LoadSqlConfigListFile();
            Dictionary<string, DataCommandConfig> result;
            if (dataCommandFileList == null || dataCommandFileList.FileList == null || dataCommandFileList.FileList.Length <= 0)
            {
                configFileList = new List<string>(1);
                result = new Dictionary<string, DataCommandConfig>(0);
            }
            else
            {
                configFileList = new List<string>(dataCommandFileList.FileList.Length + 1);
                Dictionary<string, DataCommandConfig> dictionary = new Dictionary<string, DataCommandConfig>();
                DataCommandFileList.DataCommandFile[] fileList = dataCommandFileList.FileList;
                for (int i = 0; i < fileList.Length; i++)
                {
                    DataCommandFileList.DataCommandFile dataCommandFile = fileList[i];
                    string text = dataCommandFile.FileName;
                    string pathRoot = Path.GetPathRoot(text);
                    if (pathRoot == null || pathRoot.Trim().Length <= 0)
                    {
                        text = Path.Combine(ConfigHelper.ConfigFolder, text);
                    }
                    if (!string.IsNullOrWhiteSpace(text) && File.Exists(text))
                    {
                        configFileList.Add(text);
                    }
                    DataOperations dataOperations = ConfigHelper.LoadDataCommandList(text);
                    if (dataOperations != null && dataOperations.DataCommand != null && dataOperations.DataCommand.Length > 0)
                    {
                        DataCommandConfig[] dataCommand = dataOperations.DataCommand;
                        for (int j = 0; j < dataCommand.Length; j++)
                        {
                            DataCommandConfig dataCommandConfig = dataCommand[j];
                            if (dictionary.ContainsKey(dataCommandConfig.Name))
                            {
                                throw new FileLoadException(string.Concat(new string[]
                                {
                                    "Duplicate name '",
                                    dataCommandConfig.Name,
                                    "' for data command in file '",
                                    text,
                                    "'."
                                }));
                            }
                            dictionary.Add(dataCommandConfig.Name, dataCommandConfig);
                        }
                    }
                }
                result = dictionary;
            }
            return result;
        }
       

        public static string GetDataCommandSql(string sqlNameInConfig)
        {
            Dictionary<string, DataCommandConfig> allDataCommandConfigInfosFromCache = GetAllDataCommandConfigInfosFromCache();
            if (!allDataCommandConfigInfosFromCache.ContainsKey(sqlNameInConfig))
            {
                throw new KeyNotFoundException("Can't find the data command configuration of name '" + sqlNameInConfig + "'");
            }
            var result = allDataCommandConfigInfosFromCache[sqlNameInConfig];
            return result.CommandText;
        }
        public static string FindXMLString<T>(string sqlName, T obj) where T : class
        {
            string sql = GetDataCommandSql(sqlName);
       
            var type = obj.GetType();
            PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo i in ps)
            {

                object objprop = i.GetValue(obj, null);
                string name = i.Name;
                string value = "";
                if (objprop != null)
                {
                    value = objprop.ToString();
                }
                sql = sql.Replace("@" + i.Name, value.ToString());
            }

            return sql;
        }

        public static string FindSQLString(string sqlName, QueryParameter[] parameters, out List<SqlParameter> lparm)
        {
            string sql = GetDataCommandSql(sqlName);

            string[] where;
            string orderby;
            parameters = RemoveWhereOrderDistinct(parameters, out where, out orderby);
           
            sql = GetExecuteSQL(sql, where, orderby);
            List<SqlParameter> parmlist = new List<SqlParameter>();
            foreach (var param in parameters)
            {
                parmlist.Add(new SqlParameter(param.Name, param.Value));
            }
            lparm = parmlist;
            return sql;
        }

        private static string GetExecuteSQL(string sql, string[] where = null, string orderby = null)
        {
          

            if (where != null)
            {
                for (int i = 0; i < where.Length; i++)
                {
                    sql = sql.Replace(string.Format("@{0}where", (i == 0 ? "" : i.ToString())), where[i]);
                }
            }
            if (orderby != null)
            {
                sql = sql.Replace("@orderby", orderby);
            }
            return sql;
        }



        private static QueryParameter[] RemoveWhereOrderDistinct(QueryParameter[] parameters, out string[] where, out string order)
        {
            var whereFlag = 0;
            var whereList = new List<string>();
            var orderPara = parameters.FirstOrDefault(p => p.Name == "@orderby");
            order = null;
            var paras = parameters.ToList();
            QueryParameter wherePara = null;
            do
            {
                if (wherePara != null)
                {
                    paras.Remove(wherePara);
                    whereList.Add(wherePara.Value.ToString());
                }
                wherePara = parameters.FirstOrDefault(p => p.Name == string.Format("@{0}where", (whereFlag == 0 ? "" : whereFlag.ToString())));
                whereFlag++;
            } while (wherePara != null);
            where = whereList.ToArray();

            if (orderPara != null)
            {
                paras.Remove(orderPara);
                order = orderPara.Value.ToString();
            }
            parameters = paras.ToArray();
            return parameters;
        }
    }
}
