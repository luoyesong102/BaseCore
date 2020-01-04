using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbOrderSign
    {
        public long Id { get; set; }
        public long? OrderFlowId { get; set; }
        public long? DecoratorFlowId { get; set; }
        public DateTime? AddTime { get; set; }
        public long? OptUser { get; set; }
        public string OptUsername { get; set; }
        public string EventType { get; set; }
        public string SignCustomerName { get; set; }
        public string SignCustomerTel { get; set; }
        public string SignBuildType { get; set; }
        public string SignDecorationWay { get; set; }
        public string SignAddress { get; set; }
        public string SignPrice { get; set; }
        public DateTime? SignTime { get; set; }
        public DateTime? SignStartTime { get; set; }
        public DateTime? SignFinishTime { get; set; }
        public string SignContract { get; set; }
    }
}
