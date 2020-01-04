using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SAAS.Framework.Orm.EfCore.UnitWork;
namespace SysBase.Domain.Models
{
    public partial class SysIcon: BaseEntity<int>
    {
        public string Code { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Custom { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
