
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SAAS.FrameWork;
using SysBase.Domain.Models;
using SysBase.Domain.DomainService;
using SAAS.FrameWork.Extensions;
using static Common.Service.CommonEnum;
using Common.Servicei.ViewModels.Users;
using Common.Service.DTOModel;
using Common.Service;
using Common.Service.DTOModel.Permission;

namespace Permission.Service
{
   /// <summary>
   /// 1:领域协调
   /// 2：跨领域协调
   /// 3：赋值转化为各自领域对象
   /// 4：特殊的如查询和批量可以不准守
   /// </summary>

    public class PermissionService : IPermissionService
    {

        readonly PermissionDomainService permissionservice;
        public PermissionService(PermissionDomainService _permissionservice)
        {

            permissionservice = _permissionservice;
        }
        public ApiResponsePage<SysPermission> GetPermissionList(ApiRequestPage<PermissionRequestPayload> requserobj)
        {
           return permissionservice.GetPermissionList(requserobj);
        }
        public SysPermission GetPermissionByKey(string roleid)
        {
           return permissionservice.GetPermissionByKey(roleid);
        }
        public void CreatePermission(SysPermission model)
        {
            permissionservice.CreatePermission(model);
        }
        public void EditPermission(SysPermission model)
        {
            permissionservice.EditPermission(model);
        }
        public void UpdateIsDeletePermission(CommonEnum.IsDeleted isDeleted, string ids)
        {
            permissionservice.UpdateIsDeletePermission(isDeleted, ids);
        }
        public void UpdateStatus(UserStatus status, string ids)
        {
            permissionservice.UpdateStatus(status, ids);
        }
        public List<SysPermissionWithAssignProperty> GetRolePermissionList(SysRole model)
        {
            return permissionservice.GetRolePermissionList(model);
        }
    }
}
