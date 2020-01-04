
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

namespace User.Service
{
   /// <summary>
   /// 1:领域协调
   /// 2：跨领域协调
   /// 3：赋值转化为各自领域对象
   /// 4：特殊的如查询和批量可以不准守
   /// </summary>

    public class UserService : IUserService
    {
        readonly UserDomainService userservice;
        readonly RoleDomainService roleservice;
        readonly IconDomainService iconservice;
        public UserService( UserDomainService _userservice, RoleDomainService _roleservice, IconDomainService _iconservice)
        {
            userservice = _userservice;
            roleservice = _roleservice;
            iconservice = _iconservice;
        }
        /// <summary>
        /// 获取用户列表信息
        /// </summary>
        /// <param name="requserobj"></param>
        /// <returns></returns>
        public ApiResponsePage<SysUser> UserList(ApiRequestPage requserobj)
        {
            return userservice.UserList(requserobj);
        }
      
        public List<string> GetFunctionsByUserId(Guid id)
        {
            var user = userservice.GetUserKey(id);
            List<string> roleist=  new List<string>();
            var userrolelist = user.SysUserRoleMapping.ToList();
            foreach (var item in userrolelist)
            {
                roleist.Add(item.RoleCodeNavigation.Name);
            }
            return roleservice.GetUrlList(roleist); 
        }
        public SysUser GetUserByKey(Guid userid)
        {
           return  userservice.GetUserKey(userid);
        }
        public void CreateUser(SysUser model)
        {
            userservice.CreateUser(model);
        }
        public void EditUser(SysUser model)
        {
            userservice.EditUser(model);
        }
        public void UpdateIsDeleteUser(CommonEnum.IsDeleted isDeleted, string ids)
        {
            userservice.UpdateIsDeleteUser(isDeleted, ids);
        }
        public UserContext GetUserByName(string name)
        {
          
          var userlist=  userservice.GetUser(name);
            if(userlist==null)
            {
                return null;
            }
          var userobj=  userlist.ConvertBySerialize<UserContext>();
            #region 赋值roles
            userobj.Roles = new List<string>();
           var userrolelist= userlist.SysUserRoleMapping.ToList();
            foreach (var item in userrolelist)
            {
                userobj.Roles.Add(item.RoleCodeNavigation.Name);
            }
            #endregion
            #region 赋值URL
           
            userobj.Urls = roleservice.GetUrlList(userobj.Roles);
           
            #endregion
            
            return userobj;
        }
        public List<SysPermissionWithMenu> GetPwrmissionMenuList(SysUser userobj)
        {
          return  roleservice.GetPwrmissionMenuList(userobj);
        }
        public void UpdateStatus(UserStatus status, string ids)
        {
            userservice.UpdateStatus(status, ids);
        }
        public bool SaveRoles(List<SysUserRoleMapping> model, Guid userid)
        {
         return   userservice.SaveRoles(model, userid);
        }
        public List<SysMenu> GetMenuList(UserType UserType, Guid userid)
        {
         return   roleservice.GetMenuList(UserType, userid);
        }
    }
}
