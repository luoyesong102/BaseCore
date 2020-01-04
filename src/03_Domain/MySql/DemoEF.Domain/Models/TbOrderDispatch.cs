using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderDispatch
    {
        public long Id { get; set; }
        public long? OrderFlowId { get; set; }
        public long? DecoratorUser { get; set; }
        public string DecoratorUsername { get; set; }
        public long? DecoratorFlowId { get; set; }
        public long? AssignUser { get; set; }
        public string AssignState { get; set; }
        public DateTime? AssignTime { get; set; }
        public DateTime? AskTime { get; set; }
        public int? ReceiveState { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public string ReceiveComment { get; set; }
        public DateTime? AddTime { get; set; }
        public string EventType { get; set; }
        public string OptUsername { get; set; }
        public long? OptUser { get; set; }
        public DateTime? MeasurementTime { get; set; }
        public string RejectReason { get; set; }
    }
}
