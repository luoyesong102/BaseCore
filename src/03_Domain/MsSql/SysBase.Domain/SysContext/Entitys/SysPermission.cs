using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using SAAS.Framework.Orm.EfCore.UnitWork;
using static Common.Service.CommonEnum;

namespace SysBase.Domain.Models
{
    public partial class SysPermission : BaseEntity<string>
    {
        public SysPermission()
        {
            SysRolePermissionMapping = new HashSet<SysRolePermissionMapping>();
        }
        public SysPermission(ILazyLoader LazyLoader)
        {
            _LazyLoader = LazyLoader;
        }
        private ILazyLoader _LazyLoader { get; set; }
        public Guid MenuId { get; set; }
        public string Name { get; set; }
        public string ActionCode { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        /// <summary>
        /// 权限类型(0:菜单,1:按钮/操作/功能等)
        /// </summary>
        public PermissionType Type { get; set; }
        private SysMenu _MenuGu;
        [JsonIgnore]
        public  SysMenu MenuGu
        {
            get => _LazyLoader?.Load(this, ref _MenuGu);
            set => _MenuGu = value;
        }
        [JsonIgnore]
        public  ICollection<SysRolePermissionMapping> SysRolePermissionMapping
        {
            get; set;
        }
    }
}
