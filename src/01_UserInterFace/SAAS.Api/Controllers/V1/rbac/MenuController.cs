using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controllers;
using Microsoft.AspNetCore.Mvc;
using SAAS.Api.Router;
using SAAS.FrameWork;
using SAAS.FrameWork.Module.Api.Data;
using SAAS.FrameWork.Util.SsError;
using SysBase.Domain.Models;
using User.Service;
using SAAS.FrameWork.Extensions;
using OauthService.Models.AuthContext;
using Microsoft.AspNetCore.Authorization;
using Common.Servicei.ViewModels.Users;
using Common.Service;
using Common.Service.ViewModels.Icon;
using static Common.Service.CommonEnum;
using Menu.Service;
using Common.Service.DTOModel.Menu;
using AutoMapper;

namespace SAAS.Api.Controllers.V1.Sys
{
    /// <summary>
    /// 1：菜单管理
    /// 2：业务对象的校验和验证
    /// </summary>
    [ApiVersion("1.0")]
    [CustomRoute(ApiVersions.v1, "rbac")]
    [Authorize]
    public class MenuController : BaseApiController
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;
        public MenuController(IMenuService menuService,IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="requserobj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("menulist")]
        public ApiReturn<ApiResponsePage<MenuJsonModel>> GetMenuList(ApiRequestPage<MenuRequestPayload> requserobj)
        {
            ApiResponsePage<MenuJsonModel> res = new ApiResponsePage<MenuJsonModel>();

            var sysmenu = _menuService.GetMenuList(requserobj);
            res.Count = sysmenu.Count;
            res.Body = sysmenu.Body.Select(_mapper.Map<SysMenu, MenuJsonModel>).ToList();
            return res;
          
        }
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createmenu")]
        public ApiReturn CreateMenu(MenuCreateViewModel model)
        {
            #region 校验
            if (model.Name.Trim().Length <= 0)
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "请输入角色名称"
                };

            }
            #endregion
            var entity = model.ConvertBySerialize<SysMenu>();
            entity.CreatedOn = DateTime.Now;
            entity.Id = Guid.NewGuid();
            entity.CreatedByUserGuid = AuthContextService.CurrentUser.UserId;
            entity.CreatedByUserName = AuthContextService.CurrentUser.UserName;
            entity.IsDeleted = (int)CommonEnum.IsDeleted.No;
            entity.Icon = string.IsNullOrEmpty(entity.Icon) ? "md-menu" : entity.Icon;


            _menuService.CreateMenu(entity);
            return true;

        }
        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="Id">用户GUID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("editmenu/{Id}")]
        public ApiReturn<object> Edit(Guid Id)
        {
            var entity = _menuService.GetMenuByKey(Id);
            var model = entity.ConvertBySerialize<MenuEditViewModel>();
            var tree = LoadMenuTree(model.ParentGuid.ToString());
             var res=   new { model, tree };
            return res;
        }
        /// <summary>
        /// 保存编辑后的用户信息
        /// </summary>
        /// <param name="model">用户视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatemenu")]
        public ApiReturn Editmenu(MenuEditViewModel model)
        {
            var entity = _menuService.GetMenuByKey(model.Id);
            if (entity == null)
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "用户不存在"
                };
            }

            entity.Name = model.Name;
            entity.Icon = string.IsNullOrEmpty(model.Icon) ? "md-menu" : model.Icon;
            entity.Level = 1;
            entity.ParentGuid = model.ParentGuid;
            entity.Sort = model.Sort;
            entity.Url = model.Url;
            entity.ModifiedByUserGuid = AuthContextService.CurrentUser.UserId;
            entity.ModifiedByUserName = AuthContextService.CurrentUser.UserName;
            entity.ModifiedOn = DateTime.Now;
            entity.Description = model.Description;
            entity.ParentName = model.ParentName;
            entity.Component = model.Component;
            entity.HideInMenu = (int)model.HideInMenu;
            entity.NotCache = (int)model.NotCache;
            entity.BeforeCloseFun = model.BeforeCloseFun;
            entity.Alias = model.Alias;
            entity.IsDeleted = (int)model.IsDeleted;
            entity.Status = (int)model.Status;
            entity.IsDefaultRouter = (int)model.IsDefaultRouter;

            _menuService.EditMenu(entity);
            return true;
        }

        /// <summary>
        /// 菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("menutree/{selected?}")]
        public ApiReturn<List<MenuTree>> Tree(string selected)
        {
            var tree = LoadMenuTree(selected?.ToString());
            return tree;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户GUID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [Route("deletemenu/{ids}")]
        public ApiReturn DeleteMenu(string ids)
        {
            _menuService.UpdateIsDeleteMenu(CommonEnum.IsDeleted.Yes, ids);
            return true;
        }
        /// <summary>
        /// 恢复图标
        /// </summary>
        /// <param name="ids">图标ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [Route("recovermenu/{ids}")]
        public ApiReturn Recover(string ids)
        {
            _menuService.UpdateIsDeleteMenu(CommonEnum.IsDeleted.No, ids);
            return true;
        }
        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="batchmodel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("batchmenu")]
        public ApiReturn BatchMenu(IconBatchViewModel batchmodel)
        {

            switch (batchmodel.command)
            {
                case "delete":
                    _menuService.UpdateIsDeleteMenu(CommonEnum.IsDeleted.Yes, batchmodel.ids);
                    break;
                case "recover":
                    _menuService.UpdateIsDeleteMenu(CommonEnum.IsDeleted.No, batchmodel.ids);
                    break;
                case "forbidden":
                    _menuService.UpdateStatus(UserStatus.Forbidden, batchmodel.ids);
                    break;
                case "normal":
                    _menuService.UpdateStatus(UserStatus.Normal, batchmodel.ids);
                    break;
                default:
                    break;
            }
            return true;
        }


        private List<MenuTree> LoadMenuTree(string selectedGuid = null)
        {
            var temp = _menuService.GetAllMenuList().Select(x => new MenuTree
            {
                Id = x.Id.ToString(),
                ParentGuid = x.ParentGuid,
                Title = x.Name
            }).ToList();
            var root = new MenuTree
            {
                Title = "顶级菜单",
                Id = Guid.Empty.ToString(),
                ParentGuid = null
            };
            temp.Insert(0, root);
            var tree = temp.BuildTree(selectedGuid);
            return tree;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class MenuTreeHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="selectedGuid"></param>
        /// <returns></returns>
        public static List<MenuTree> BuildTree(this List<MenuTree> menus, string selectedGuid = null)
        {
            var lookup = menus.ToLookup(x => x.ParentGuid);
            Func<Guid?, List<MenuTree>> build = null;
            build = pid =>
            {
                return lookup[pid]
                 .Select(x => new MenuTree()
                 {
                     Id = x.Id,
                     ParentGuid = x.ParentGuid,
                     Title = x.Title,
                     Expand = (x.ParentGuid == null || x.ParentGuid == Guid.Empty),
                     Selected = selectedGuid == x.Id,
                     Children = build(new Guid(x.Id)),
                 })
                 .ToList();
            };
            var result = build(null);
            return result;
        }
    }
}