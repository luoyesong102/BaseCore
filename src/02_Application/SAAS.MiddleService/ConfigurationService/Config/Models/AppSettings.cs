namespace ConfigurationService
{
    #region 日志对象
    /// <summary>
    /// 日志对象
    /// </summary>
    public class Logging
    {
        /// <summary>
        /// 日志对象
        /// </summary>
        public LogLevel loglevel { get; set; }
    }
    /// <summary>
    /// 日志级别
    /// </summary>
    public class LogLevel
    {
        /// <summary>
        /// 是否默认
        /// </summary>
        public string Default { get; set; }
    }
    #endregion
    #region SQL管理配置
    /// <summary>
    /// SQL管理
    /// </summary>
    public class DataAccessSetting
    {
        /// <summary>
        /// SQL管理路径
        /// </summary>
        public string SqlConfigListFilePath { get; set; }
    }
    #endregion
    #region 数据库配置
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// redis连接字符串
        /// </summary>
        public string Redis { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string Order_Db { get; set; }
        /// <summary>
        /// Mssql数据库连接字符串
        /// </summary>
        public string Sys_Db { get; set; }
    }
    #endregion
    #region 认证配置项


    /// <summary>
    /// 认证配置项
    /// </summary>
    public class Jwt
    {
        /// <summary>
        /// 发行人yuiter.com
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 接受者yuiter.com
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 秘钥a48fafeefd334237c2ca207e842afe0b
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// 过期时间（分钟）
        /// </summary>
        public int ExpireMinutes { get; set; }
       
    }
    #endregion
    #region 依赖注入集合
    /// <summary>
    /// 依赖注入集合
    /// </summary>
    public class AssemblyListModel
    {
        /// <summary>
        /// 仓储的程序集
        /// </summary>
        public string RepositoryAssembly { get; set; }
        /// <summary>
        /// 服务的程序集
        /// </summary>
        public string FunctionAssembly { get; set; }
        
    }
    #endregion
    #region 系统Key对象
    /// <summary>
    /// 系统Key对象
    /// </summary>
    public class ObjAttr
    {
        /// <summary>
        /// 服务编号
        /// </summary>
        public int ServerNodeNo { get; set; }
       
    }
    #endregion

}
