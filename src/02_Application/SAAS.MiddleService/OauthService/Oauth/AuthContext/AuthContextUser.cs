/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * 版权所有，请勿删除
 ******************************************/

using System;
using System.Collections.Generic;
using static Common.Service.CommonEnum;

namespace OauthService.Models.AuthContext
{
   
    /// <summary>
    /// 用户经过token认证后的信息
    /// </summary>
    public class JwtAuthorizationModel
    {
        /// <summary>
        /// 刷新Token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public long Expires { get; set; }

        /// <summary>
        /// 授权时间
        /// </summary>
        public long Ouths { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 用户主键
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 授权类型
        /// </summary>
        public string TokenType{ get; set; } = "Bearer";
}


    public class OauthUserContext
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }
        public List<string> Rolelist { get; set; }

    }


}