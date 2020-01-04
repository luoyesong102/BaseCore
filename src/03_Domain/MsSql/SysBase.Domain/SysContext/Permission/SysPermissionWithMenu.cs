/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * 版权所有，请勿删除
 ******************************************/

using System;
using static Common.Service.CommonEnum;

namespace SysBase.Domain.Models
{
    /// <summary>
    /// 权限实体类
    /// </summary>
    public class SysPermissionWithMenu
    {
        /// <summary>
        /// 权限码
        /// </summary>
        public string PermissionId { get; set; }
        /// <summary>
        /// 权限操作码
        /// </summary>
        public string PermissionActionCode { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PermissionType PermissionType { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单GUID
        /// </summary>
        public Guid MenuId { get; set; }
        /// <summary>
        /// 菜单别名(与前端路由配置中的name值保持一致)
        /// </summary>
        public string MenuAlias { get; set; }
        /// <summary>
        /// 是否是默认前端路由
        /// </summary>
        public YesOrNo IsDefaultRouter { get; set; }
    }
}