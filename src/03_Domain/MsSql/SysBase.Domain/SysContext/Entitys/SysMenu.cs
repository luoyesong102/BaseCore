using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SAAS.Framework.Orm.EfCore.UnitWork;
namespace SysBase.Domain.Models
{
    public partial class SysMenu: BaseEntity<Guid>
    {
        public SysMenu()
        {
            SysPermission = new HashSet<SysPermission>();
        }
      
        public string Name { get; set; }
        public string Url { get; set; }
        public string Alias { get; set; }
        public string Icon { get; set; }
        public Guid? ParentGuid { get; set; }
        public string ParentName { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public int Sort { get; set; }
        public int Status { get; set; }
      
        public int IsDefaultRouter { get; set; }
       
        public string Component { get; set; }
        public int? HideInMenu { get; set; }
        public int? NotCache { get; set; }
        public string BeforeCloseFun { get; set; }
       
        public  ICollection<SysPermission> SysPermission
        { get; set; }
    }
}
