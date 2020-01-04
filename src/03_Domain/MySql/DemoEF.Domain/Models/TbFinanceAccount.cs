using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbFinanceAccount
    {
        public long Id { get; set; }
        public long? DecoratorUser { get; set; }
        public string DecoratorUsername { get; set; }
        public string Remarks { get; set; }
        public string AccountStatus { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string AccountPwd { get; set; }
    }
}
