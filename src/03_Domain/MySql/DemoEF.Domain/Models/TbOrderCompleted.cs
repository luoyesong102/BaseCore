using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderCompleted
    {
        public long Id { get; set; }
        public long? OrderFlowId { get; set; }
        public long? DecoratorFlowId { get; set; }
        public DateTime? AddTime { get; set; }
        public long? OptUser { get; set; }
        public string OptUsername { get; set; }
        public string EventType { get; set; }
        public DateTime? CompletedStartWorktime { get; set; }
        public DateTime? CompletedFinishWorktime { get; set; }
        public string CompletedPictureurl { get; set; }
    }
}
