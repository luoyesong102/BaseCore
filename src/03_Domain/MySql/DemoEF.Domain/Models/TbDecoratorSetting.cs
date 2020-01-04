using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbDecoratorSetting
    {
        public long Id { get; set; }
        public long? DecoratorUser { get; set; }
        public string DecoratorUsername { get; set; }
        public string Jsontext { get; set; }
        public string CompanyLevel { get; set; }
        public DateTime? AddTime { get; set; }
        public int? DayPolicy { get; set; }
        public int? MonthPolicy { get; set; }
        public int? DecoratorStatus { get; set; }
        public string Address { get; set; }
        public string OtherJsontext { get; set; }
        public string OppUser { get; set; }
    }
}
