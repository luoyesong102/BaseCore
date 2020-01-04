/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * 版权所有，请勿删除
 ******************************************/

using Common.Service.DTOModel.Menu;
using Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OauthService.Models.AuthContext;
using SAAS.Api.Router;
using SAAS.FrameWork.Module.Api.Data;
using SysBase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using User.Service;
using static Common.Service.CommonEnum;

namespace SAAS.Api
{
    /// <summary>
    /// 登录及菜单权限列表
    /// </summary>
    [ApiVersion("1.0")]
    [CustomRoute(ApiVersions.v1, "account")]
    public class AccountController :  BaseApiController
    {
        private readonly IUserService _userService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getUserInfo")]
        public ApiReturn<object> getUserInfo()
        {
             var guid = AuthContextService.CurrentUser.UserId;
             var user=   _userService.GetUserByKey(guid);
             var pagePermissions=  _userService.GetPwrmissionMenuList(user);
            var datas = new
            {
                access = new string[] { },
                avator = user.Avatar,
                user_guid = AuthContextService.CurrentUser.UserId,
                user_name = user.DisplayName,
                user_type = user.UserType,
                permissions = pagePermissions

            };
            return datas;
        }
        /// <summary>
        /// 获取权限菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Menu")]
        public ApiReturn<List<MenuItem>> Menu()
        {
           var usertype = AuthContextService.CurrentUser.UserType;
            var userid = AuthContextService.CurrentUser.UserId;
            var menus = _userService.GetMenuList(usertype, userid);
             var menu = MenuItemHelper.LoadMenuTree(menus, "0");
            return menu;
        }
   

}

    public static class MenuItemHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="selectedGuid"></param>
        /// <returns></returns>
        public static List<MenuItem> BuildTree(this List<MenuItem> menus, string selectedGuid = null)
        {
            var lookup = menus.ToLookup(x => x.ParentId);

            List<MenuItem> Build(string pid)
            {
                return lookup[pid]
                    .Select(x => new MenuItem()
                    {
                        Id = x.Id,
                        ParentId = x.ParentId,
                        Children = Build(x.Id),
                        Component = x.Component ?? "Main",
                        Name = x.Name,
                        Path = x.Path,
                        Meta = new MenuMeta
                        {
                            BeforeCloseFun = x.Meta.BeforeCloseFun,
                            HideInMenu = x.Meta.HideInMenu,
                            Icon = x.Meta.Icon,
                            NotCache = x.Meta.NotCache,
                            Title = x.Meta.Title,
                            Permission = x.Meta.Permission
                        }
                    }).ToList();
            }

            var result = Build(selectedGuid);
            return result;
        }

        public static List<MenuItem> LoadMenuTree(List<SysMenu> menus, string selectedGuid = null)
        {
            var temp = menus.Select(x => new MenuItem
            {
                Id = x.Id.ToString(),
                ParentId = x.ParentGuid != null && ((Guid)x.ParentGuid) == Guid.Empty ? "0" : x.ParentGuid?.ToString(),
                Name = x.Alias,
                Path = $"/{x.Url}",
                Component = x.Component,
                Meta = new MenuMeta
                {
                    BeforeCloseFun = x.BeforeCloseFun ?? "",
                    HideInMenu = x.HideInMenu == (int)YesOrNo.Yes,
                    Icon = x.Icon,
                    NotCache = x.NotCache == (int)YesOrNo.Yes,
                    Title = x.Name
                }
            }).ToList();
            var tree = temp.BuildTree(selectedGuid);
            return tree;
        }
    }
}