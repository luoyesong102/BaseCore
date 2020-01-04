
using ConfigurationService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace CacheSqlXmlService.SqlManager
{
    internal static class ConfigHelper
    {
        //private const string DEFAULT_SQL_CONFIG_LIST_FILE_PATH = "Configuration/Data/DbCommandFiles.config";//没用到，支持linux的路径写法
        private static string s_ConfigFolder = null;
        private static DataAccessSetting s_Setting = ConfigurationManage.dataAccessSetting;//ConfigurationManager.Instance.GetByPath<DataAccessSetting>("DataAccessSetting");


        public static string ConfigFolder
        {
            get
            {
                if (ConfigHelper.s_ConfigFolder == null)
                {
                    ConfigHelper.s_ConfigFolder = Path.GetDirectoryName(ConfigHelper.SqlConfigListFilePath);
                }
                return ConfigHelper.s_ConfigFolder;
            }
        }
        public static string SqlConfigListFilePath
        {
            get
            {
                string raletepath = Path.Combine( "Configuration", "Data", "DbCommandFiles.config");//linux和windwos同时支持
                 string text = s_Setting.SqlConfigListFilePath?? raletepath;

                string pathRoot = Path.GetPathRoot(text);
                string result;
                if (pathRoot == null || pathRoot.Trim().Length <= 0)
                {
                    result = Path.Combine(Directory.GetCurrentDirectory(), "Configuration", "Data", "DbCommandFiles.config");//linux和windwos同时支持
                }
                else
                {
                    result = text;
                }
               
                return result;
            }
        }
        
        private static T LoadFromXml<T>(string fileName)
        {
            FileStream fileStream = null;
            T result;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                result = (T)((object)xmlSerializer.Deserialize(fileStream));
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }
            return result;
        }
        public static DataCommandFileList LoadSqlConfigListFile()
        {
            string sqlConfigListFilePath = ConfigHelper.SqlConfigListFilePath;
            DataCommandFileList result;
            if (!string.IsNullOrWhiteSpace(sqlConfigListFilePath) && File.Exists(sqlConfigListFilePath.Trim()))
            {
                result = ConfigHelper.LoadFromXml<DataCommandFileList>(sqlConfigListFilePath.Trim());
            }
            else
            {
                result = null;
            }
            return result;
        }
        
        public static DataOperations LoadDataCommandList(string filePath)
        {
            return ConfigHelper.LoadFromXml<DataOperations>(filePath);
        }

     
    }
}
