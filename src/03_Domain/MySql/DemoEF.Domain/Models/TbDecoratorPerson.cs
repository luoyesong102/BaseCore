using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbDecoratorPerson
    {
        public long Id { get; set; }
        public long? DecoratorUser { get; set; }
        public string DecoratorUsername { get; set; }
        public string Position { get; set; }
        public string PersonTel { get; set; }
        public string Person { get; set; }
        public DateTime? AddTime { get; set; }
        public string OppUser { get; set; }
    }
}
