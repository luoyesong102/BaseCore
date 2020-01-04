using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderRefund
    {
        public long Id { get; set; }
        public long? OrderFlowId { get; set; }
        public long? DecoratorFlowId { get; set; }
        public string EventType { get; set; }
        public DateTime? AddTime { get; set; }
        public long? OptUser { get; set; }
        public string OptUsername { get; set; }
        public string RefundContent { get; set; }
        public string Prove { get; set; }
        public string RefundReason { get; set; }
        public int? RefundStatus { get; set; }
        public string AuditId { get; set; }
        public string DecoratorUsername { get; set; }
        public long? DecoratorUser { get; set; }
        public long? BillId { get; set; }
    }
}
