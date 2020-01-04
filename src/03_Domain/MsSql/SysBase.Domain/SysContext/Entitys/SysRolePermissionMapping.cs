using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SAAS.Framework.Orm.EfCore.UnitWork;
namespace SysBase.Domain.Models
{
    public partial class SysRolePermissionMapping : BaseEntity
    {
        public SysRolePermissionMapping()
        {
         
        }
        public SysRolePermissionMapping(ILazyLoader LazyLoader)
        {
            _LazyLoader = LazyLoader;
        }
        private ILazyLoader _LazyLoader { get; set; }
        public string RoleId { get; set; }
        public string PermissionId { get; set; }
        private SysPermission _PermissionCodeNavigation;
        private SysRole _RoleCodeNavigation;
        public  SysPermission PermissionCodeNavigation
        {
            get => _LazyLoader?.Load(this, ref _PermissionCodeNavigation);
            set => _PermissionCodeNavigation = value;
        }
        public  SysRole RoleCodeNavigation
        {
            get => _LazyLoader?.Load(this, ref _RoleCodeNavigation);
            set => _RoleCodeNavigation = value;
        }
    }
}
