using Microsoft.Extensions.Configuration;
using System.IO;
using System.Configuration;

namespace ConfigurationService
{
    /// <summary>
    /// 配置文件管理 所有配置文件从此处取值统一管理 弃用1：ConfigurationManager.Instance.GetByPath<DataAccessSetting>("DataAccessSetting");2 构造函数注入Iconfigureation 3 serveice.configureation   ioptoion的方式，
    /// </summary>
    public class ConfigurationManage
   {
        #region Initialize

        /// <summary>
        /// 加锁防止并发操作
        /// </summary>
        private static readonly object _locker = new object();

        /// <summary>
        /// 配置实例
        /// </summary>
        private static ConfigurationManage _instance = null;

        /// <summary>
        /// 配置根节点
        /// </summary>
        private IConfiguration Config { get; }

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ConfigurationManage()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Config = builder.Build();
        }

        /// <summary>
        /// 获取配置实例
        /// </summary>
        /// <returns></returns>
        private static ConfigurationManage GetInstance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigurationManage();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region 暂时不对外开放

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="name">配置节点名称</param>
        /// <returns></returns>
        private static string GetConfig(string name)
        {
            return GetInstance().Config.GetSection(name).Value;
        }

        #endregion
        /// <summary>
        /// 读取配置文件[Logging]节点数据:日志对象
        /// </summary>
        public static Logging logging
        {
            get
            {
                var logging = new Logging();
                GetInstance().Config.GetSection("Logging").Bind(logging);
                return logging;
            }
        }
        /// <summary>
        /// 读取配置文件[DataAccessSetting]节点数据:SQL管理配置
        /// </summary>
        public static DataAccessSetting dataAccessSetting
        {
            get
            {
                var dataAccessSetting = new DataAccessSetting();
                GetInstance().Config.GetSection("DataAccessSetting").Bind(dataAccessSetting);
                return dataAccessSetting;
            }
        }
        /// <summary>
        /// 读取配置文件[ConnectionStrings]节点数据:数据库配置
        /// </summary>
        public static ConnectionStrings connectionStrings
        {
            get
            {
                var connectionStrings = new ConnectionStrings();
                GetInstance().Config.GetSection("ConnectionStrings").Bind(connectionStrings);
                return connectionStrings;
            }
        }
        /// <summary>
        /// 读取配置文件[JwtIssuerOptions]节点数据:认证配置项
        /// </summary>
        public static Jwt jwt
        {
            get
            {
                var jwt = new Jwt();
                GetInstance().Config.GetSection("Jwt").Bind(jwt);
                return jwt;
            }
        }
        /// <summary>
        /// 读取配置文件[AssemblyListModel]节点数据:动态查询依赖注入集合
        /// </summary>
        public static AssemblyListModel assemblyListModel
        {
            get
            {
                var assemblyListModel = new AssemblyListModel();

                GetInstance().Config.GetSection("AssemblyListModel").Bind(assemblyListModel);
                return assemblyListModel;
            }
        }
    }
}
