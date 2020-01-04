using CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SAAS.FrameWork;
using SysBase.Domain.Models;
using static Common.Service.CommonEnum;
using Common.Servicei.ViewModels.Users;
using Common.Service.DTOModel;
using Common.Service;
using Common.Service.DTOModel.Permission;

namespace Permission.Service
{
    public interface IPermissionService : AutoInject
    {
       
        ApiResponsePage<SysPermission> GetPermissionList(ApiRequestPage<PermissionRequestPayload> requserobj);
        SysPermission GetPermissionByKey(string roleid);
        void CreatePermission(SysPermission model);
        void EditPermission(SysPermission model);
        void UpdateIsDeletePermission(CommonEnum.IsDeleted isDeleted, string ids);
        void UpdateStatus(UserStatus status, string ids);
        List<SysPermissionWithAssignProperty> GetRolePermissionList(SysRole model);
    }

  
}
