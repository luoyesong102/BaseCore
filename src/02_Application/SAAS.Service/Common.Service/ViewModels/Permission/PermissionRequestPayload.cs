/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * 版权所有，请勿删除
 ******************************************/

using Common.Servicei.ViewModels.Users;
using System;
using static Common.Service.CommonEnum;

namespace Common.Service.DTOModel.Permission
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionRequestPayload : CommonModel
    {
        /// <summary>
        /// 关联菜单GUID
        /// </summary>
        public Guid? MenuId { get; set; }
    }
}
