using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderOperation
    {
        public long Id { get; set; }
        public string OrginData { get; set; }
        public string CurData { get; set; }
        public string EventType { get; set; }
        public DateTime? AddTime { get; set; }
        public long? OptUser { get; set; }
        public string OptUsername { get; set; }
        public string Remarks { get; set; }
        public long? OrderFlowId { get; set; }
        public string DecoratorFlowId { get; set; }
        public string OptRole { get; set; }
        public string OrderNo { get; set; }
    }
}
