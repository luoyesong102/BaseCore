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
using Permission.Service;
using Common.Service.DTOModel.Permission;
using SAAS.FrameWork.Util.Common;
using Menu.Service;
using AutoMapper;

namespace SAAS.Api.Controllers.V1.Sys
{
    /// <summary>
    /// 1：权限管理
    /// 2：业务对象的校验和验证
    /// </summary>
    [ApiVersion("1.0")]
    [CustomRoute(ApiVersions.v1, "rbac")]
    [Authorize]
    public class PermissionController : BaseApiController
    {
        private readonly IPermissionService _permissionService;
        private readonly IRoleService _roleService;
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;

        public PermissionController(IPermissionService permissionService, IRoleService roleService, IMenuService menuService, IMapper mapper)
        {
            _permissionService = permissionService;
            _roleService = roleService;
            _menuService = menuService;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取图标列表
        /// </summary>
        /// <param name="requserobj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("permissionlist")]
        public ApiReturn<ApiResponsePage<PermissionJsonModel>> GetPermissionList(ApiRequestPage<PermissionRequestPayload> requserobj)
        {
            ApiResponsePage<PermissionJsonModel> res = new ApiResponsePage<PermissionJsonModel>();
          
            var permissionlist= _permissionService.GetPermissionList(requserobj);
            res.Count = permissionlist.Count;
            res.Body = permissionlist.Body.Select(_mapper.Map<SysPermission, PermissionJsonModel>).ToList();
            return res;
        }
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createpermission")]
        public ApiReturn Createpermission(PermissionCreateViewModel model)
        {
            #region 校验
            if (model.Name.Trim().Length <= 0)
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "请输入权限名称"
                };

            }
            #endregion
            var entity = model.ConvertBySerialize<SysPermission>();
            entity.CreatedOn = DateTime.Now;
            entity.Id = RandomHelper.GetRandomizer(8, true, false, true, true);
           
            entity.CreatedByUserGuid = AuthContextService.CurrentUser.UserId;
            entity.CreatedByUserName = AuthContextService.CurrentUser.UserName;
            _permissionService.CreatePermission(entity);
            return true;

        }
        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="Id">用户GUID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("editpermission/{Id}")]
        public ApiReturn<PermissionEditViewModel> Edit(string Id)
        {
            var entity = _permissionService.GetPermissionByKey(Id);
            var viewuser = entity.ConvertBySerialize<PermissionEditViewModel>();
            viewuser.MenuName = entity.MenuGu.Name;
            return viewuser;
        }
        /// <summary>
        /// 保存编辑后的用户信息
        /// </summary>
        /// <param name="model">用户视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatepermission")]
        public ApiReturn EditPermission(PermissionEditViewModel model)
        {
            var entity = _permissionService.GetPermissionByKey(model.Id);
            if (entity == null)
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "权限不存在"
                };
            }
            entity.Name = model.Name;
            entity.ActionCode = model.ActionCode;
            entity.MenuId = model.MenuId;
            entity.IsDeleted = (int)model.IsDeleted;
            entity.ModifiedByUserGuid = AuthContextService.CurrentUser.UserId;
            entity.ModifiedByUserName = AuthContextService.CurrentUser.UserName;
            entity.ModifiedOn = DateTime.Now;
            entity.Status = (int)model.Status;
            entity.Description = model.Description;

            

            _permissionService.EditPermission(entity);
            return true;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户GUID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [Route("deletepermission/{ids}")]
        public ApiReturn DeletePermission(string ids)
        {
            _permissionService.UpdateIsDeletePermission(CommonEnum.IsDeleted.Yes, ids);
            return true;
        }
        /// <summary>
        /// 恢复图标
        /// </summary>
        /// <param name="ids">图标ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [Route("recoverpermission/{ids}")]
        public ApiReturn Recover(string ids)
        {
            _permissionService.UpdateIsDeletePermission(CommonEnum.IsDeleted.No, ids);
            return true;
        }
        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="batchmodel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("batchpermission")]
        public ApiReturn BatchPermission(IconBatchViewModel batchmodel)
        {

            switch (batchmodel.command)
            {
                case "delete":
                    _permissionService.UpdateIsDeletePermission(CommonEnum.IsDeleted.Yes, batchmodel.ids);
                    break;
                case "recover":
                    _permissionService.UpdateIsDeletePermission(CommonEnum.IsDeleted.No, batchmodel.ids);
                    break;
                case "forbidden":
                    _permissionService.UpdateStatus(UserStatus.Forbidden, batchmodel.ids);
                    break;
                case "normal":
                    _permissionService.UpdateStatus(UserStatus.Normal, batchmodel.ids);
                    break;
                default:
                    break;
            }
            return true;
        }

        /// <summary>
        /// 角色-权限菜单树
        /// </summary>
        /// <param name="id">角色编码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("permissiontree/{roleid}")]
        public ApiReturn<object> PermissionTree(string roleid)
        {
          var role=  _roleService.GetRoleByKey(roleid);
            if (role == null)
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "权限不存在"
                };
              
            }
            var menu = _menuService.GetAllMenuList().OrderBy(x => x.CreatedOn).ThenBy(x => x.Sort)
                    .Select(x => new PermissionMenuTree
                    {
                        Id = x.Id,
                        ParentGuid = x.ParentGuid,
                        Title = x.Name
                    }).ToList();

            var permissionList =  _permissionService.GetRolePermissionList(role);
            var tree = menu.FillRecursive(permissionList, Guid.Empty, role.IsSuperAdministrator);
            var result = new { tree, selectedPermissions = permissionList.Where(x => x.IsAssigned == 1).Select(x => x.Id) };
            return result;
        }

    }
    public static class PermissionTreeHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menus">菜单集合</param>
        /// <param name="permissions">权限集合</param>
        /// <param name="parentGuid">父级菜单GUID</param>
        /// <param name="isSuperAdministrator">是否为超级管理员角色</param>
        /// <returns></returns>
        public static List<PermissionMenuTree> FillRecursive(this List<PermissionMenuTree> menus, List<SysPermissionWithAssignProperty> permissions, Guid? parentGuid, bool isSuperAdministrator = false)
        {
            List<PermissionMenuTree> recursiveObjects = new List<PermissionMenuTree>();
            foreach (var item in menus.Where(x => x.ParentGuid == parentGuid))
            {
                var children = new PermissionMenuTree
                {
                    AllAssigned = isSuperAdministrator ? true : (permissions.Where(x => x.MenuId == item.Id).Count(x => x.IsAssigned == 0) == 0),
                    Expand = true,
                    Id = item.Id,
                    ParentGuid = item.ParentGuid,
                    Permissions = permissions.Where(x => x.MenuId == item.Id).Select(x => new PermissionElement
                    {
                        Name = x.Name,
                        Id = x.Id,
                        IsAssignedToRole = IsAssigned(x.IsAssigned, isSuperAdministrator)
                    }).ToList(),

                    Title = item.Title,
                    Children = FillRecursive(menus, permissions, item.Id)
                };
                recursiveObjects.Add(children);
            }
            return recursiveObjects;
        }

        private static bool IsAssigned(int isAssigned, bool isSuperAdministrator)
        {
            if (isSuperAdministrator)
            {
                return true;
            }
            return isAssigned == 1;
        }

        //public static List<PermissionMenuTree> FillRecursive(this List<PermissionMenuTree> menus, List<DncPermissionWithAssignProperty> permissions, Guid? parentGuid)
        //{
        //    List<PermissionMenuTree> recursiveObjects = new List<PermissionMenuTree>();

        //    return recursiveObjects;
        //}
    }
}