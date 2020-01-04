using System;
using System.Collections.Generic;
using System.Text;

namespace SAAS.FrameWork.RabbitMq
{
    public class ActionLog
    {
        public string ActionLogId { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
