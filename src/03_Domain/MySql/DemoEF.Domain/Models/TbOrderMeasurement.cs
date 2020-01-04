using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderMeasurement
    {
        public long Id { get; set; }
        public long? OrderFlowId { get; set; }
        public long? DecoratorFlowId { get; set; }
        public DateTime? AddTime { get; set; }
        public long? OptUser { get; set; }
        public string OptUsername { get; set; }
        public string EventType { get; set; }
        public string MeasurementState { get; set; }
        public string MeasurementIntention { get; set; }
        public DateTime? MeasurementFlowupTime { get; set; }
        public string MeasurementAddress { get; set; }
        public DateTime? MeasurementAppointmentTime { get; set; }
        public string MeasurementRecordtxt { get; set; }
        public DateTime? MeasurementNextflowupTime { get; set; }
        public DateTime? MeasurementTime { get; set; }
        public string MeasurementRecordtype { get; set; }
    }
}
