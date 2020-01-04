using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderServicesign
    {
        public long Id { get; set; }
        public DateTime? AddTime { get; set; }
        public long? OptUser { get; set; }
        public string OptUsername { get; set; }
        public string EventType { get; set; }
        public long? OrderFlowId { get; set; }
        public long? DecoratorUser { get; set; }
        public string DecoratorUsername { get; set; }
        public string ServiceRebateRate { get; set; }
        public decimal? ServiceSignMoney { get; set; }
        public DateTime? ServiceSignTime { get; set; }
    }
}
