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
using Common.Service.DTOModel.Role;
using SAAS.FrameWork.Util.Common;
using AutoMapper;

namespace SAAS.Api.Controllers.V1.Sys
{
    /// <summary>
    /// 1：角色管理
    /// 2：业务对象的校验和验证
    /// </summary>
    [ApiVersion("1.0")]
    [CustomRoute(ApiVersions.v1, "rbac")]
    [Authorize]
    public class RoleController : BaseApiController
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取指定用户的角色列表
        /// </summary>
        /// <param name="guid">用户GUID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("findlistbyuserid/{guid}")]
        public ApiReturn<object> FindListByUserGuid(Guid guid)
        {
            var roles = _roleService.GetAllRoleList().Select(x => new { label = x.Name, key = x.Id }); ;
            var assignedRoles = _roleService.GetUserRoleList(guid).Select(x => x.Id).ToList();
           return new { roles, assignedRoles }.Serialize();
        }
        /// <summary>
        /// 获取图标列表
        /// </summary>
        /// <param name="requserobj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("rolelist")]
        public ApiReturn<ApiResponsePage<RoleJsonModel>> GetRoleList(ApiRequestPage<CommonModel> requserobj)
        {
            
            ApiResponsePage<RoleJsonModel> res = new ApiResponsePage<RoleJsonModel>();
            var rolelist=  _roleService.GetRoleList(requserobj);
            res.Count = rolelist.Count;
            res.Body = rolelist.Body.Select(_mapper.Map<SysRole, RoleJsonModel>).ToList();
            return res;
        }
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("createrole")]
        public ApiReturn CreateRole(RoleCreateViewModel model)
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
            var entity = model.ConvertBySerialize<SysRole>();
            entity.CreatedOn = DateTime.Now;
            entity.Id = RandomHelper.GetRandomizer(8, true, false, true, true);
            entity.IsSuperAdministrator = false;
            entity.IsBuiltin = false;

            entity.CreatedByUserGuid = AuthContextService.CurrentUser.UserId;
            entity.CreatedByUserName = AuthContextService.CurrentUser.UserName;
            _roleService.CreateRole(entity);
            return true;

        }
        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="Id">用户GUID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("editrole/{Id}")]
        public ApiReturn<RoleEditViewModel> Edit(string Id)
        {
            var entity = _roleService.GetRoleByKey(Id);
            var viewuser = entity.ConvertBySerialize<RoleEditViewModel>();
            return viewuser;
        }
        /// <summary>
        /// 保存编辑后的用户信息
        /// </summary>
        /// <param name="model">用户视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [Route("updaterole")]
        public ApiReturn EditRole(RoleEditViewModel model)
        {
            var entity = _roleService.GetRoleByKey(model.Id);
            if (entity == null)
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "用户不存在"
                };
            }
            entity.Name = model.Name;
            entity.IsDeleted = (int)model.IsDeleted;
            entity.ModifiedByUserGuid = AuthContextService.CurrentUser.UserId;
            entity.ModifiedByUserName = AuthContextService.CurrentUser.UserName;
            entity.ModifiedOn = DateTime.Now;
            entity.Status = (int)model.Status;
            entity.Description = model.Description;

            _roleService.EditRole(entity, AuthContextService.IsSupperAdministator);
            return true;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户GUID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [Route("deleterole/{ids}")]
        public ApiReturn DeleteRole(string ids)
        {
            _roleService.UpdateIsDeleteRole(CommonEnum.IsDeleted.Yes, ids);
            return true;
        }
        /// <summary>
        /// 恢复图标
        /// </summary>
        /// <param name="ids">图标ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [Route("recoverrole/{ids}")]
        public ApiReturn Recover(string ids)
        {
            _roleService.UpdateIsDeleteRole(CommonEnum.IsDeleted.No, ids);
            return true;
        }
        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="batchmodel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("batchrole")]
        public ApiReturn BatchRole(IconBatchViewModel batchmodel)
        {

            switch (batchmodel.command)
            {
                case "delete":
                    _roleService.UpdateIsDeleteRole(CommonEnum.IsDeleted.Yes, batchmodel.ids);
                    break;
                case "recover":
                    _roleService.UpdateIsDeleteRole(CommonEnum.IsDeleted.No, batchmodel.ids);
                    break;
                case "forbidden":
                    _roleService.UpdateStatus(UserStatus.Forbidden, batchmodel.ids);
                    break;
                case "normal":
                    _roleService.UpdateStatus(UserStatus.Normal, batchmodel.ids);
                    break;
                default:
                    break;
            }
            return true;
        }

        /// <summary>
        /// 查询所有角色列表(只包含主要的字段信息:name,code)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("findsimplelist")]
        public ApiReturn<object> FindSimpleList()
        {
            var roles = _roleService.GetAllRoleList().Where(x => x.IsDeleted == (int)CommonEnum.IsDeleted.No && x.Status == (int)CommonEnum.Status.Normal).Select(x => new { x.Name, x.Id }).ToList(); ;
            return roles;


        }

        /// <summary>
        /// 为指定角色分配权限
        /// </summary>
        /// <param name="payload">角色分配权限的请求载体类</param>
        /// <returns></returns>
        [HttpPost]
        [Route("assignpermission")]
        public ApiReturn AssignPermission(RoleAssignPermissionPayload payload)
        {
          var role=  _roleService.GetRoleByKey(payload.RoleId);
            if (role == null)
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "角色不存在"
                };
               
            }
            // 如果是超级管理员，则不写入到角色-权限映射表(在读取时跳过角色权限映射，直接读取系统所有的权限)
            if (role.IsSuperAdministrator)
            {
                return true;
            }
            _roleService.AssignPermission(role, payload);
            return true;
        }
    }
}