using Common.Service;
using Common.Service.DTOModel.Permission;
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
     public  class PermissionDomainService
    {
        private readonly ISysRepositoryBase<SysPermission> _permissionRepository; 
        private readonly SysUnitOfWork _unitOfWork;  
        public PermissionDomainService(ISysRepositoryBase<SysPermission> permissionRepository, SysUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }

        public ApiResponsePage<SysPermission> GetPermissionList(ApiRequestPage<PermissionRequestPayload> requserobj)
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
            var query = _permissionRepository.DBContext.SysPermission.AsQueryable();
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
            var dataRequest = new ApiRequestPage<IQueryable<SysPermission>>()
            {
                PageIndex = requserobj.PageIndex,
                PageSize = requserobj.PageSize,
                OrderBy = requserobj.OrderBy,
                SortCol = requserobj.SortCol,
                Where = query
            };
            ApiResponsePage<SysPermission> pageuserlist = _permissionRepository.ToPage(dataRequest);
            #endregion
            return pageuserlist;
        }
        public SysPermission GetPermissionByKey(string roleid)
        {
            return _permissionRepository.GetByKey(roleid);
        }
        public void CreatePermission(SysPermission model)
        {
            if (model.Name.Trim().Length <= 0)
            {
                throw new BaseException("请输入权限名称");
            }
            if (_permissionRepository.IsExist<SysPermission>(x => x.ActionCode == model.ActionCode && x.MenuId == model.MenuId))
            {
                throw new BaseException("权限操作码已存在");
            }
            _permissionRepository.Add(model);
            _unitOfWork.SaveChanges();
        }
        public void EditPermission(SysPermission model)
        {

            if (_permissionRepository.IsExist<SysPermission>(x => x.ActionCode == model.ActionCode && x.Id != model.Id) )
            {
                throw new BaseException("权限操作码已存在");
               
            }
            if (model == null)
            {
                throw new BaseException("权限不存在");
            }
            _permissionRepository.Update(model);
            _unitOfWork.SaveChanges();
        }
        public void UpdateIsDeletePermission(CommonEnum.IsDeleted isDeleted, string ids)
        {
            var parameters = ids.Split(',').Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
            var sql = string.Format("UPDATE sys_Permission SET IsDeleted=@IsDeleted WHERE id IN ({0})", parameterNames);
            parameters.Add(new SqlParameter("@IsDeleted", (int)isDeleted));
            _permissionRepository.ExecuteSql(sql, parameters);
            _unitOfWork.SaveChanges();
        }
        public void UpdateStatus(UserStatus status, string ids)
        {
            var parameters = ids.Split(',').Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
            var sql = string.Format("UPDATE sys_Permission SET Status=@Status WHERE id IN ({0})", parameterNames);
            parameters.Add(new SqlParameter("@Status", status));
            _permissionRepository.ExecuteSql(sql, parameters);
            _unitOfWork.SaveChanges();
        }
        public List<SysPermissionWithAssignProperty> GetRolePermissionList(SysRole model)
        {
            var sql = @"SELECT P.Id,P.MenuId,P.Name,P.ActionCode,ISNULL(S.RoleId,'') AS RoleId,(CASE WHEN S.PermissionId IS NOT NULL THEN 1 ELSE 0 END) AS IsAssigned FROM Sys_Permission AS P 
                        LEFT JOIN (SELECT * FROM Sys_RolePermissionMapping AS RPM WHERE RPM.RoleId={0}) AS S 
                        ON S.PermissionId= P.Id
                        WHERE P.IsDeleted=0 AND P.Status=1";
            if (model.IsSuperAdministrator)
            {
                sql = @"SELECT P.Id,P.MenuId,P.Name,P.ActionCode,'SUPERADM' AS RoleId,(CASE WHEN P.Id IS NOT NULL THEN 1 ELSE 0 END) AS IsAssigned FROM Sys_Permission AS P 
                        WHERE P.IsDeleted=0 AND P.Status=1";
            }
            var permissionList = _permissionRepository.DBContext.SysPermissionWithAssignProperty.FromSql(sql, model.Id).ToList();
            return permissionList;
        }
    }
}