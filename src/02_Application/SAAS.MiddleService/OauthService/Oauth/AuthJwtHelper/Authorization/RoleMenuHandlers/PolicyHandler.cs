//-----------------------------------------------------------------------
// <copyright file= "PolicyHandler.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/15 19:23:35 
// Modified by:
// Description: 角色策略授权处理
//-----------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Grapefruit.WebApi.Handlers
{
    public class PolicyHandler : AuthorizationHandler<PolicyRequirement>
    {
        /// <summary>
        /// 授权方式（cookie, bearer, oauth, openid）
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }



        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="schemes"></param>
        /// <param name="jwtApp"></param>
        public PolicyHandler(IAuthenticationSchemeProvider schemes)
        {
            Schemes = schemes;
           
        }

        //授权处理
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            //Todo：获取角色、Url 对应关系
            List<Menu> list = new List<Menu> {
                new Menu
                {
                    Role = Guid.Empty.ToString(),
                    Url = "/api/v1.0/Values"
                },
                new Menu
                {
                    Role=Guid.Empty.ToString(),
                    Url="/api/v1.0/secret/deactivate"
                },
                new Menu
                {
                    Role=Guid.Empty.ToString(),
                    Url="/api/v1.0/secret/refresh"
                }
            };

            var httpContext = (context.Resource as AuthorizationFilterContext).HttpContext;
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
           
                if (isAuthenticated)
                {
                    //判断角色与 Url 是否对应
                    //
                    var url = httpContext.Request.Path.Value.ToLower();
                    var role = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value;
                    var menu = list.Where(x => x.Role.Equals(role) && x.Url.ToLower().Equals(url)).FirstOrDefault();

                    if (menu == null)
                    {
                        context.Fail();
                        return;
                    }
                    //判断是否过期
                    if (DateTime.Parse(httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration).Value) >= DateTime.UtcNow)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                    return;
                }
            context.Fail();
        }

        /// <summary>
        /// 测试菜单类
        /// </summary>
        public class Menu
        {
            public string Role { get; set; }

            public string Url { get; set; }
        }
    }
}
