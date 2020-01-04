using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SAAS.Framework.Orm.EfCore.UnitWork;
using SysBase.Domain.SysContext;

namespace SysBase.Domain.Models
{
    public partial class SysUserRoleMapping: BaseEntity
    {
        public SysUserRoleMapping()
        {
          
        }
        public SysUserRoleMapping(ILazyLoader LazyLoader)
        {
            _LazyLoader = LazyLoader;
        }
        private ILazyLoader _LazyLoader { get; set; }
        public Guid UserId { get; set; }
        public string RoleId { get; set; }

        private SysRole _RoleCodeNavigation;
        public  SysRole RoleCodeNavigation
        {
            get => _LazyLoader?.Load(this, ref _RoleCodeNavigation);
            set => _RoleCodeNavigation = value;
        }
        private SysUser _UserGu;
        public  SysUser UserGu
        {
            get => _LazyLoader?.Load(this, ref _UserGu);
            set => _UserGu = value;
        }
    }
}
