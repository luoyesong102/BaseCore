using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderSupervise
    {
        public long Id { get; set; }
        public long? OrderFlowId { get; set; }
        public long? DecoratorFlowId { get; set; }
        public DateTime? AddTime { get; set; }
        public long? OptUser { get; set; }
        public string OptUsername { get; set; }
        public string EventType { get; set; }
        public string SuperviseState { get; set; }
        public string SuperviseContent { get; set; }
    }
}
