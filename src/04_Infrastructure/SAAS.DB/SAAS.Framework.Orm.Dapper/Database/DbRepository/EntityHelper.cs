using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SAAS.DB.Dapper.Repository
{
   public static class EntityHelper
    {
        /// <summary>
        /// 获取实体主键名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetPrimaryKey<T>()
        {
            var primary = typeof(T).GetCustomAttributes(typeof(KeyAttribute), true);
            var pri = typeof(T).GetProperties();
            foreach (var i in pri)
            {
                var pris = i.GetCustomAttributes(typeof(KeyAttribute), true);
                if (pris.Any())
                {
                    return i.Name;
                }
            }
            return "";
        }
        /// <summary>
        /// 获取实体代表的表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetTableName<T>()
        {
            var tablename = typeof(T).GetCustomAttributes(typeof(TableAttribute), true);
            return ((TableAttribute)tablename[0]).Name;
        }

        public static string GetTableName(Type entityType)
        {
            try
            {
                var tablename = entityType.GetCustomAttributes(typeof(TableAttribute), true);
                return ((TableAttribute)tablename[0]).Name;
            }
            catch
            {
                throw new Exception("没有配置TableName特性！");
            }

        }
    }
}