using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderAssign
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long DecoratorUser { get; set; }
        public string DecoratorUsername { get; set; }
        public long? AssignUser { get; set; }
        public int? AssignState { get; set; }
        public DateTime? AssignTime { get; set; }
        public DateTime? AskTime { get; set; }
        public int? ReceiveState { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public string ReceiveComment { get; set; }
        public int? MeasurementState { get; set; }
        public DateTime? MeasurementTime { get; set; }
        public string MeasurementComment { get; set; }
        public int? SignState { get; set; }
        public DateTime? PhoneBridgeExpireTime { get; set; }
        public string DecoratorTels { get; set; }
        public int? ChargebackState { get; set; }
        public DateTime? ChargebackApplyTime { get; set; }
        public string ChargebackApplyComment { get; set; }
        public int? PhoneBridgeCount { get; set; }
    }
}
