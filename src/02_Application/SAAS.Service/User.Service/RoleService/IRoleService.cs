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
using Common.Service.DTOModel.Role;

namespace Menu.Service
{
    public interface IRoleService : AutoInject
    {
        List<SysRole> GetUserRoleList( Guid userid);
        List<SysRole> GetAllRoleList();
        ApiResponsePage<SysRole> GetRoleList(ApiRequestPage<CommonModel> requserobj);
        SysRole GetRoleByKey(string roleid);
        void CreateRole(SysRole model);
        void EditRole(SysRole model, bool IsSupperAdministator);
        void UpdateIsDeleteRole(CommonEnum.IsDeleted isDeleted, string ids);
        void UpdateStatus(UserStatus status, string ids);
        void AssignPermission(SysRole model, RoleAssignPermissionPayload payload);
    }

  
}
