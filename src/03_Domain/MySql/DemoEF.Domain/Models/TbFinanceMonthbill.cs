using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbFinanceMonthbill
    {
        public long Id { get; set; }
        public long? AccountId { get; set; }
        public long? DecoratorUser { get; set; }
        public string DecoratorUsername { get; set; }
        public DateTime? Monthdate { get; set; }
        public decimal? PreviousBalance { get; set; }
        public decimal? Balance { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? AddTime { get; set; }
    }
}
