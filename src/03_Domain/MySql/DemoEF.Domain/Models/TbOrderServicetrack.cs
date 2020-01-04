using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderServicetrack
    {
        public long Id { get; set; }
        public long? OrderFlowId { get; set; }
        public long? TrackUser { get; set; }
        public string TrackUsername { get; set; }
        public DateTime? TrackTime { get; set; }
        public string TrackComment { get; set; }
        public long? DecoratorFlowId { get; set; }
        public string EventType { get; set; }
    }
}
