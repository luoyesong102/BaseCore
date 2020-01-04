using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAAS.FrameWork
{
    public enum ServerErrcodeEnum : int
    {
        Normal = 0,
        /// <summary>
        /// 服务异常
        /// </summary>
        ServiceError = 1,
        /// <summary>
        /// 登录异常
        /// </summary>
        LoginError = 2,
        /// <summary>
        /// tocken验证失败
        /// </summary>
        TokenError = 3,

        /// <summary>
        /// 码不存在
        /// </summary>
        BusError = 4,
        /// <summary>
        /// 码被使用
        /// </summary>
        BusCodeDoError = 5,
        /// <summary>
        /// 码未被激活
        /// </summary>
        BusCodeUndoError = 6,
        /// <summary>
        /// 该项目无路由
        /// </summary>
        BusProjectNoRouterError = 7,
        /// <summary>
        /// 当前码无路由
        /// </summary>
        BusCodeNoRouterError = 8
    }
    public enum LogType
    {
        Mongodb = 1,
        Redis = 2,
        Track = 3,
        Queue = 4,
        IPCheck = 5,
        ES = 6,
        JS = 7,
        Other = 0,
        Rabbitmq = 8,
        Error = 9,
        Warring = 10,
    }

    public enum LoggerType
    {
        WebExceptionLog,
        ServiceExceptionLog,
        Error,
        Warnning,
        Info,
        Trace
    }

    /// <summary>
    /// 排序
    /// </summary>
    public enum Sort
    {
        Asc = 0,
        Desc = 1
    }
}
