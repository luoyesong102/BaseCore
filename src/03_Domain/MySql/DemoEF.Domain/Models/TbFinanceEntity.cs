using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbFinanceEntity
    {
        public long Id { get; set; }
        public long? AccountId { get; set; }
        public long? DecoratorUser { get; set; }
        public string DecoratorUsername { get; set; }
        public int? EntityType { get; set; }
        public DateTime? EndPolicytime { get; set; }
        public DateTime? StartPolicytime { get; set; }
        public int? Dopolicycount { get; set; }
        public decimal? Balance { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
