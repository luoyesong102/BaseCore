using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderRefundAudit
    {
        public long Id { get; set; }
        public string RefundId { get; set; }
        public long? AuditPerson { get; set; }
        public int? AuditResult { get; set; }
        public string AuditRemark { get; set; }
        public DateTime? AuditTime { get; set; }
    }
}
