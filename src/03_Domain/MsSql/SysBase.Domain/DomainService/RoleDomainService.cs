using Common.Service;
using Common.Service.DTOModel.Role;
using Common.Servicei.ViewModels.Users;
using Microsoft.EntityFrameworkCore;
using SAAS.FrameWork;
using SAAS.FrameWork.IOC;
using SAAS.FrameWork.Util.Exp;
using SysBase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Service.CommonEnum;

namespace SysBase.Domain.DomainService
{
     public  class RoleDomainService
    {
        private readonly ISysRepositoryBase<SysRole> _roleRepository;
  
        private readonly SysUnitOfWork _unitOfWork;  
        public RoleDomainService(ISysRepositoryBase<SysRole> roleRepository, SysUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 根据角色集合获取角色对象列表
        /// </summary>
        /// <param name="rolelist"></param>
        /// <returns></returns>
        public List<SysRole> GetRoleList(List<string> rolelist)
        {

            var sysmodel = _roleRepository.Get(u => rolelist.Contains(u.Id)).ToList();
            return sysmodel;
        }
        public List<SysRole> GetUserRoleList(Guid userid)
        {
            //有N+1次查询的性能问题
            //var query = _dbContext.DncUser
            //    .Include(r => r.UserRoles)
            //    .ThenInclude(x => x.DncRole)
            //    .Where(x => x.Guid == guid);
            //var roles = query.FirstOrDefault().UserRoles.Select(x => new
            //{
            //    x.DncRole.Code,
            //    x.DncRole.Name
            //});
            var sql = @"SELECT R.* FROM Sys_UserRoleMapping AS URM
                     INNER JOIN Sys_Role AS R ON R.Id=URM.RoleId
                     WHERE URM.UserId={0}";
           return _roleRepository.LoadAllBySql<SysRole>(sql,userid).ToList();  
        }
        public List<SysRole> GetAllRoleList()
        {
           return _roleRepository.Get(x => x.IsDeleted == (int)CommonEnum.IsDeleted.No && x.Status == (int)CommonEnum.Status.Normal).ToList();
        }
        public ApiResponsePage<SysRole> GetRoleList(ApiRequestPage<CommonModel> requserobj)
        {
            #region 默认排序
            Dictionary<int, string> orderDictionary = new Dictionary<int, string>
            {
                {1, "Id"},
            };
            if (string.IsNullOrEmpty(requserobj.OrderBy))
            {
                requserobj.OrderBy = orderDictionary.FirstOrDefault().Value;
            }
            #endregion
            #region 动态条件查询
            var query = _roleRepository.DBContext.SysRole.AsQueryable();
            if (!string.IsNullOrEmpty(requserobj.Kw))
            {
                query = query.Where(x => x.Id.Contains(requserobj.Kw.Trim()) || x.Name.Contains(requserobj.Kw.Trim()));
            }
            if (requserobj.Where.IsDeleted > CommonEnum.IsDeleted.All)
            {
                query = query.Where(x => x.IsDeleted == (int)requserobj.Where.IsDeleted);
            }
            if (requserobj.Where.Status > UserStatus.All)
            {
                query = query.Where(x => x.Status == (int)requserobj.Where.Status);
            }
            #endregion
            #region 分页查询
            var dataRequest = new ApiRequestPage<IQueryable<SysRole>>()
            {
                PageIndex = requserobj.PageIndex,
                PageSize = requserobj.PageSize,
                OrderBy = requserobj.OrderBy,
                SortCol = requserobj.SortCol,
                Where = query
            };
            ApiResponsePage<SysRole> pageuserlist = _roleRepository.ToPage(dataRequest);
            #endregion
            return pageuserlist;
        }
        public SysRole GetRoleByKey(string roleid)
        {
            var sysmodel = _roleRepository.GetByKey(roleid);
            return sysmodel;
        }
        public void CreateRole(SysRole model)
        {
            if (_roleRepository.IsExist<SysRole>(x => x.Name == model.Name))
            {
                throw new BaseException("该角色已存在");
            }
            _roleRepository.Add(model);
            _unitOfWork.SaveChanges();
        }
        public void EditRole(SysRole model,bool IsSupperAdministator)
        {
            if (_roleRepository.IsExist<SysRole>(x => x.Name == model.Name&& x.Id != model.Id))
            {
                throw new BaseException("该角色已存在");
            }

            if (model.IsSuperAdministrator && !IsSupperAdministator)
            {
                throw new BaseException("没有足够的权限");
            }
            _roleRepository.Update(model);
            _unitOfWork.SaveChanges();
        }
        public void UpdateIsDeleteRole(CommonEnum.IsDeleted isDeleted, string ids)
        {
            var parameters = ids.Split(',').Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
            var sql = string.Format("UPDATE Sys_Role SET IsDeleted=@IsDeleted WHERE id IN ({0})", parameterNames);
            parameters.Add(new SqlParameter("@IsDeleted", (int)isDeleted));
            _roleRepository.ExecuteSql(sql, parameters);
            _unitOfWork.SaveChanges();
        }
        public void UpdateStatus(UserStatus status, string ids)
        {
            var parameters = ids.Split(',').Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
            var sql = string.Format("UPDATE Sys_Role SET Status=@Status WHERE id IN ({0})", parameterNames);
            parameters.Add(new SqlParameter("@Status", (int)status));
            _roleRepository.ExecuteSql(sql, parameters);
            _unitOfWork.SaveChanges();
        }
        public void AssignPermission(SysRole model, RoleAssignPermissionPayload payload)
        {
            _roleRepository.DBContext.Database.ExecuteSqlCommand("DELETE FROM Sys_RolePermissionMapping WHERE RoleId={0}", model.Id);
            if (payload.Permissions != null || payload.Permissions.Count > 0)
            {
                var permissions = payload.Permissions.Select(x => new SysRolePermissionMapping
                {
                    CreatedOn = DateTime.Now,
                    PermissionId = x.Trim(),
                    RoleId = payload.RoleId.Trim()
                });
                _roleRepository.DBContext.SysRolePermissionMapping.AddRange(permissions);
                _unitOfWork.SaveChanges();
            }
        }
        /// <summary>
        /// 根据角色获取角色下URL集合列表
        /// </summary>
        /// <param name="rolelist"></param>
        /// <returns></returns>
        public List<string> GetUrlList(List<string> rolelist)
        {
            var results = from role in _roleRepository.DBContext.SysRole
                          join rolepermission in _roleRepository.DBContext.SysRolePermissionMapping on role.Id equals rolepermission.RoleId 
                          join  permission in _roleRepository.DBContext.SysPermission on rolepermission.PermissionId equals permission.Id
                          join menu in _roleRepository.DBContext.SysMenu on permission.MenuId equals menu.Id
                          where rolelist.Contains(role.Id)
                          select menu;
            return results.Select(u=>u.Url).ToList();


        }
        /// <summary>
        /// 获取某人菜单集合
        /// </summary>
        /// <param name="userobj"></param>
        /// <returns></returns>
        public List<SysPermissionWithMenu> GetPwrmissionMenuList(SysUser userobj)
        {
            //查询当前登录用户拥有的权限集合(非超级管理员)
            var sqlPermission = @"SELECT P.Id AS PermissionId,P.ActionCode AS PermissionActionCode,P.Name AS PermissionName,P.Type AS PermissionType,M.Name AS MenuName,M.Id AS MenuId,M.Alias AS MenuAlias,M.IsDefaultRouter FROM Sys_RolePermissionMapping AS RPM 
                                LEFT JOIN Sys_Permission AS P ON P.Id = RPM.PermissionId
                                INNER JOIN Sys_Menu AS M ON M.Id = P.MenuId
                                WHERE P.IsDeleted=0 AND P.Status=1 AND EXISTS (SELECT 1 FROM Sys_UserRoleMapping AS URM WHERE URM.UserId={0} AND URM.RoleId=RPM.RoleId)";
            if (userobj.UserType == UserType.SuperAdministrator)
            {
                //如果是超级管理员
                sqlPermission = @"SELECT P.Id AS PermissionId,P.ActionCode AS PermissionActionCode,P.Name AS PermissionName,P.Type AS PermissionType,M.Name AS MenuName,M.Id AS MenuId,M.Alias AS MenuAlias,M.IsDefaultRouter FROM Sys_Permission AS P 
                INNER JOIN Sys_Menu AS M ON M.Id = P.MenuId
                WHERE P.IsDeleted=0 AND P.Status=1";
            }
            var permissions = _roleRepository.DBContext.SysPermissionWithMenu.FromSql(sqlPermission, userobj.Id).ToList();
            return permissions;
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="UserType"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<SysMenu> GetMenuList(UserType UserType,Guid userid)
        {
            var strSql = @"SELECT M.* FROM Sys_RolePermissionMapping AS RPM 
                            LEFT JOIN Sys_Permission AS P ON P.Id = RPM.PermissionId
                            INNER JOIN Sys_Menu AS M ON M.Id = P.MenuId
                            WHERE P.IsDeleted=0 AND P.Status=1 AND P.Type=0 AND M.IsDeleted=0 AND M.Status=1 AND EXISTS (SELECT 1 FROM Sys_UserRoleMapping AS URM WHERE URM.UserId={0} AND URM.RoleId=RPM.RoleId)";

            if (UserType == UserType.SuperAdministrator)
            {
                //如果是超级管理员
                strSql = @"SELECT * FROM Sys_Menu WHERE IsDeleted=0 AND Status=1";
            }
            var menus = _roleRepository.DBContext.SysMenu.FromSql(strSql, userid).ToList();
            var rootMenus = _roleRepository.DBContext.SysMenu.Where(x => x.IsDeleted == (int)IsDeleted.No && x.Status == (int)Status.Normal && x.ParentGuid == Guid.Empty).ToList();
            foreach (var root in rootMenus)
            {
                if (menus.Exists(x => x.Id == root.Id))
                {
                    continue;
                }
                menus.Add(root);
            }
            menus = menus.OrderBy(x => x.Sort).ThenBy(x => x.CreatedOn).ToList();
            return menus;
        }
    }
}