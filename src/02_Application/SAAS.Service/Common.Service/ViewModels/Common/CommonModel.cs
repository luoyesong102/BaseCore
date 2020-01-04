/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * DESCRIPTION:     用户信息实体类
 ******************************************/
using System;
using static Common.Service.CommonEnum;

namespace Common.Servicei.ViewModels.Users
{
    /// <summary>
    /// 公共字段
    /// </summary>
    public class CommonModel
    {
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public IsDeleted IsDeleted { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStatus Status { get; set; }
    }
    public class PicData
    {
        public string Msg { get; set; }
    }
}
