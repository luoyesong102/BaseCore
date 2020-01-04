
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
using Common.Service.DTOModel.Role;

namespace Menu.Service
{
   /// <summary>
   /// 1:领域协调
   /// 2：跨领域协调
   /// 3：赋值转化为各自领域对象
   /// 4：特殊的如查询和批量可以不准守
   /// </summary>

    public class RoleService : IRoleService
    {
        readonly RoleDomainService roleservice;
        public RoleService( RoleDomainService _roleservice)
        {
          
            roleservice = _roleservice;
           
        }
        public List<SysRole> GetUserRoleList(Guid userid)
        {
         return   roleservice.GetUserRoleList(userid);
        }
        public List<SysRole> GetAllRoleList()
        {
            return roleservice.GetAllRoleList();
        }
      public  ApiResponsePage<SysRole> GetRoleList(ApiRequestPage<CommonModel> requserobj)
        {
         return   roleservice.GetRoleList(requserobj);
        }

        public SysRole GetRoleByKey(string roleid)
        {
          return  roleservice.GetRoleByKey(roleid);
        }
        public void CreateRole(SysRole model)
        {
             roleservice.CreateRole(model);
        }
        public void EditRole(SysRole model, bool IsSupperAdministator)
        {
             roleservice.EditRole(model, IsSupperAdministator);
        }
        public void UpdateIsDeleteRole(CommonEnum.IsDeleted isDeleted, string ids)
        {
             roleservice.UpdateIsDeleteRole(isDeleted, ids);
        }
        public void UpdateStatus(UserStatus status, string ids)
        {
             roleservice.UpdateStatus(status, ids);
        }

        public void AssignPermission(SysRole model, RoleAssignPermissionPayload payload)
        {
            roleservice.AssignPermission(model,payload);
        }
    }
}
