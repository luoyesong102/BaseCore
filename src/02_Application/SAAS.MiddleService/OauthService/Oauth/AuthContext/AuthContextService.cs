/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * 版权所有，请勿删除
 ******************************************/


using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using static Common.Service.CommonEnum;

namespace OauthService.Models.AuthContext
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuthContextService
    {
        private static IHttpContextAccessor _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }
        /// <summary>
        /// 
        /// </summary>
        public static HttpContext Current => _context.HttpContext;
        /// <summary>
        /// 
        /// </summary>
        public static OauthUserContext CurrentUser
        {
            get
            {
                var user = new OauthUserContext
                {
                    UserId = Guid.Parse(Current.User.FindFirstValue("id")),
                    UserName = Current.User.FindFirstValue(ClaimTypes.Name),
                    Email = Current.User.FindFirstValue(ClaimTypes.Email),
                    Phone = Current.User.FindFirstValue(ClaimTypes.MobilePhone),
                    //Rolelist= Current.User.FindAll(ClaimTypes.Role).ToList(),
                    UserType = (UserType)Convert.ToInt32(Current.User.FindFirstValue("userType"))

                };
                return user;
            }
        }
        

/// <summary>
/// 是否已授权
/// </summary>
public static bool IsAuthenticated
        {
            get
            {
                return Current.User.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public static bool IsSupperAdministator
        {
            get
            {
                return ((UserType)Convert.ToInt32(Current.User.FindFirstValue("userType"))== UserType.SuperAdministrator);
            }
        }
    }
}


//var serviceProvider = app.ApplicationServices;
//var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
//AuthContextService.Configure(httpContextAccessor);