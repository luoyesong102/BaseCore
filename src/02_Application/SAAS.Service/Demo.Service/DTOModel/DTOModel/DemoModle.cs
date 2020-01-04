using System;
using System.Collections.Generic;

namespace Demo.Service.DTO
{
    public partial class DemoModle
    { 
        public long Id { get; set; }
        public long AppId { get; set; }
        public long MaterialId { get; set; }
        public long AllocationCount { get; set; }
        public long WarningCount { get; set; }
        public sbyte Status { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
