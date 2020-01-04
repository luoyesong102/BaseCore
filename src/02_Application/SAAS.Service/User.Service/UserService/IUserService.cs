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

namespace User.Service
{
    public interface IUserService : AutoInject
    {
        ApiResponsePage<SysUser> UserList(ApiRequestPage requserobj);
       
        
        UserContext GetUserByName(string name);
        SysUser GetUserByKey(Guid userid);
        void CreateUser(SysUser model);
        void EditUser(SysUser model);
        void UpdateIsDeleteUser(CommonEnum.IsDeleted isDeleted, string ids);
        void UpdateStatus(UserStatus status, string ids);
        bool SaveRoles(List<SysUserRoleMapping> model ,Guid userid);
        List<string> GetFunctionsByUserId(Guid id);
        List<SysPermissionWithMenu> GetPwrmissionMenuList(SysUser userobj);
        List<SysMenu> GetMenuList(UserType UserType, Guid userid);
    }

  
}
