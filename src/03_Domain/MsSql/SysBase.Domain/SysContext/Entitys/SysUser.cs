using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using SAAS.Framework.Orm.EfCore.UnitWork;
using SysBase.Domain.SysContext;
using static Common.Service.CommonEnum;

namespace SysBase.Domain.Models
{
    public partial class SysUser : BaseEntity<Guid>
    {
        public SysUser()
        {
         
            SysUserRoleMapping = new HashSet<SysUserRoleMapping>();
        }
        public SysUser(ILazyLoader LazyLoader)
        {
            _LazyLoader = LazyLoader;
        }
        private ILazyLoader _LazyLoader { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public UserType UserType { get; set; }
        public int IsLocked { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Description { get; set; }
        private ICollection<SysUserRoleMapping> _SysUserRoleMapping;
        [JsonIgnore]
        public  ICollection<SysUserRoleMapping> SysUserRoleMapping
        {
            get => _LazyLoader?.Load(this, ref _SysUserRoleMapping);
            set => _SysUserRoleMapping = value;
        }
    }
}
