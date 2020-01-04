using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderTrack
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long TrackUser { get; set; }
        public string TrackUsername { get; set; }
        public DateTime TrackTime { get; set; }
        public string TrackComment { get; set; }
        public string NextTrackTime { get; set; }
    }
}
