using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderStruck
    {
        public long Id { get; set; }
        public long? OrderFlowId { get; set; }
        public long? DecoratorFlowId { get; set; }
        public DateTime? AddTime { get; set; }
        public long? OptUser { get; set; }
        public string OptUsername { get; set; }
        public string EventType { get; set; }
        public string StruckState { get; set; }
        public string StruckIntention { get; set; }
        public string StruckRecordtype { get; set; }
        public string StruckProgress { get; set; }
        public DateTime? StruckFlowupTime { get; set; }
        public string StruckRecordtxt { get; set; }
        public DateTime? StruckNextflowupTime { get; set; }
    }
}
