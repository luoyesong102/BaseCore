using System;
using System.Collections.Generic;
using System.Text;

namespace SAAS.Mq.Socket.Channel
{
    enum EMsgType : byte
    {
        /// <summary>
        /// request
        /// </summary>
        request = 1,
        /// <summary>
        /// reply
        /// </summary>
        reply = 2,
        /// <summary>
        /// 单向数据
        /// </summary>
        message=3
    }
}
