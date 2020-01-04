using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderDecoratorFlow
    {
        public long Id { get; set; }
        public long? OrderFlowId { get; set; }
        public string AssignType { get; set; }
        public long? DecoratorUser { get; set; }
        public string DecoratorUsername { get; set; }
        public int? CurDecoreatorstate { get; set; }
        public string DesignerBridgetel { get; set; }
        public string DesignerTel { get; set; }
        public string Designer { get; set; }
        public string DesignDirectorBridgetel { get; set; }
        public string DesignDirectorTel { get; set; }
        public string ServiceName { get; set; }
        public string DesignDirector { get; set; }
        public string ServiceBridgetel { get; set; }
        public string ServiceTel { get; set; }
        public int? PhoneBridgeCount { get; set; }
        public DateTime? PhoneBridgeExpireTime { get; set; }
        public long? DispatchId { get; set; }
        public long? StruckId { get; set; }
        public string MeasurementId { get; set; }
        public long? FeedbackId { get; set; }
        public long? SignId { get; set; }
        public long? CompletedId { get; set; }
        public long? SuperviseId { get; set; }
        public long? WastageId { get; set; }
        public long? RefundId { get; set; }
        public long? ActiveId { get; set; }
        public int? WastageState { get; set; }
        public DateTime? AskTime { get; set; }
        public string AssignState { get; set; }
        public DateTime? AssignTime { get; set; }
        public long? AssignUser { get; set; }
        public int? ReceiveState { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public int? MeasurementState { get; set; }
        public DateTime? MeasurementTime { get; set; }
        public int? SignState { get; set; }
        public DateTime? SignTime { get; set; }
        public int? RefundState { get; set; }
        public DateTime? ActiveTime { get; set; }
        public DateTime? FeedbackTime { get; set; }
        public DateTime? WastageTime { get; set; }
        public DateTime? StruckTime { get; set; }
        public DateTime? CompleteTime { get; set; }
        public DateTime? SuperviseTime { get; set; }
        public DateTime? StruckNextflowupTime { get; set; }
        public DateTime? StartoperationTime { get; set; }
        public int? CompleteState { get; set; }
        public int? StartoperationState { get; set; }
        public int? SuperviseState { get; set; }
        public int? StruckState { get; set; }
        public int? DesignateState { get; set; }
        public long? SnapshotId { get; set; }
        public string ReceiveComment { get; set; }
        public string MeasurementComment { get; set; }
        public DateTime? MeasurementNextflowupTime { get; set; }
    }
}
