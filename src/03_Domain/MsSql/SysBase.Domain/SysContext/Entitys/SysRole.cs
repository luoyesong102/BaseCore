using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SAAS.Framework.Orm.EfCore.UnitWork;
namespace SysBase.Domain.Models
{
    public partial class SysRole : BaseEntity<string>
    {
        public SysRole()
        {
            SysRolePermissionMapping = new HashSet<SysRolePermissionMapping>();
            SysUserRoleMapping = new HashSet<SysUserRoleMapping>();
        }
       
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
       
        public bool IsSuperAdministrator { get; set; }
        public bool IsBuiltin { get; set; }
        public  ICollection<SysRolePermissionMapping> SysRolePermissionMapping
        { get; set; }
        public  ICollection<SysUserRoleMapping> SysUserRoleMapping
        { get; set; }
    }
}
