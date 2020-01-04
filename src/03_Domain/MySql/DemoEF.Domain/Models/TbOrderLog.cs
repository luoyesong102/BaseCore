using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderLog
    {
        public long Id { get; set; }
        public long? OrderId { get; set; }
        public long? OrderAssignId { get; set; }
        public DateTime AddTime { get; set; }
        public long OptUser { get; set; }
        public string OptUsername { get; set; }
        public string Content { get; set; }
        public string EventCategory { get; set; }
        public string EventType { get; set; }
        public string FlowRemark { get; set; }
        public string ExtData { get; set; }
    }
}
