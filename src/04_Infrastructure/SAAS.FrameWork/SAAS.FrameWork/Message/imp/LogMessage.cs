using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SAAS.FrameWork.Message
{
    /// <summary>
    /// Sql的xml文件发生了改变时产生的Message
    /// </summary>
    public class LogExpMessage<T> : IMessage
    {
        private T _dto;

        public LogExpMessage() { }


        public LogExpMessage(T dto) {
            _dto = dto;
        }


        /// <summary>
        /// 处理这此消息，将改变的Xml重新序列化并保存到缓存中
        /// </summary>
        public virtual void ProcessMe()
        {
            
        }

     
    }

    
}
