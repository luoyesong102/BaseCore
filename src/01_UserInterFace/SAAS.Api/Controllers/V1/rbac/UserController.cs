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
using AutoMapper;

namespace SAAS.Api.Controllers.V1.Sys
{
    /// <summary>
    /// 1：用户管理
    /// 2：业务对象的校验和验证
    /// </summary>
    [ApiVersion("1.0")]
    [CustomRoute(ApiVersions.v1, "rbac")]
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController( IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("userlist")]
        public ApiReturn<ApiResponsePage<UserJsonModel>> GetDetail(ApiRequestPage requserobj)
        {
            ApiResponsePage<UserJsonModel> res = new ApiResponsePage<UserJsonModel>();
            var userlist = _userService.UserList(requserobj);
            res.Count = userlist.Count;
            res.Body = userlist.Body.Select(_mapper.Map<SysUser, UserJsonModel>).ToList();
            return res;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="model">用户视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [Route("createuser")]
        public ApiReturn CreateUser(UserCreateViewModel model)
        {
            #region 校验
            if (model.UserName.Trim().Length <= 0)
            {
                 return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "请输入登录名称"
                 };
               
            }
            #endregion
            var  sysuser = model.ConvertBySerialize<SysUser>();
            sysuser.Id = Guid.NewGuid();
            sysuser.CreatedOn = DateTime.Now;
            sysuser.CreatedByUserGuid = AuthContextService.CurrentUser.UserId;
            sysuser.CreatedByUserName = AuthContextService.CurrentUser.UserName;
            _userService.CreateUser(sysuser);
            return true;
           
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="Id">用户GUID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("edituser/{Id}")]
        public ApiReturn<UserEditViewModel> Edit(Guid Id)
        {
            var entity = _userService.GetUserByKey(Id);
            var viewuser = entity.ConvertBySerialize<UserEditViewModel>();
            return viewuser;
        }
        /// <summary>
        /// 保存编辑后的用户信息
        /// </summary>
        /// <param name="model">用户视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateuser")]
        public ApiReturn EditUser(UserEditViewModel model)
        {
            var entity = _userService.GetUserByKey(model.Id);
            if (entity == null)
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "用户不存在"
                };
            }
            entity.DisplayName = model.DisplayName;
            entity.IsDeleted = (int)model.IsDeleted;
            entity.IsLocked = (int)model.IsLocked;
            entity.ModifiedByUserGuid = AuthContextService.CurrentUser.UserId;
            entity.ModifiedByUserName = AuthContextService.CurrentUser.UserName;
            entity.ModifiedOn = DateTime.Now;
            entity.Password = model.Password;
            entity.Status = (int)model.Status;
            entity.UserType = model.UserType;
            entity.Description = model.Description;
            _userService.EditUser(entity);
            return true;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户GUID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [Route("deleteuser/{ids}")]
        public ApiReturn DeleteUser(string ids)
        {
            _userService.UpdateIsDeleteUser(CommonEnum.IsDeleted.Yes, ids);
            return true;
        }
        /// <summary>
        /// 恢复图标
        /// </summary>
        /// <param name="ids">图标ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [Route("recoveruser/{ids}")]
        public ApiReturn Recover(string ids)
        {
            _userService.UpdateIsDeleteUser(CommonEnum.IsDeleted.No, ids);
            return true;
        }
        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="batchmodel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("batchuser")]
        public ApiReturn BatchUser(IconBatchViewModel batchmodel)
        {

            switch (batchmodel.command)
            {
                case "delete":
                    _userService.UpdateIsDeleteUser(CommonEnum.IsDeleted.Yes, batchmodel.ids);
                    break;
                case "recover":
                    _userService.UpdateIsDeleteUser(CommonEnum.IsDeleted.No, batchmodel.ids);
                    break;
                case "forbidden":
                    _userService.UpdateStatus(UserStatus.Forbidden, batchmodel.ids);
                    break;
                case "normal":
                    _userService.UpdateStatus(UserStatus.Normal, batchmodel.ids);
                    break;
                default:
                    break;
            }
            return true;
        }
        #region 用户-角色
        /// <summary>
        /// 保存用户-角色的关系映射数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("saveroles")]
        public ApiReturn SaveRoles(SaveUserRolesViewModel model)
        {
           
             var roles = model.AssignedRoles.Select(x => new SysUserRoleMapping
            {
                UserId = model.UserId,
                CreatedOn = DateTime.Now,
                RoleId = x.Trim()
            }).ToList();
            bool issave = _userService.SaveRoles(roles, model.UserId);

            if (issave)
            {
                return true;
            }
            else
            {
                return new SsError
                {
                    errorCode = 2000,
                    errorMessage = "保存用户角色数据失败"
                };
                
            }
          
        }
        #endregion
    }
}