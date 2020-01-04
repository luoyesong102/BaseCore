using Common.Service;
using Common.Service.DTOModel.Menu;
using SAAS.FrameWork;
using SAAS.FrameWork.IOC;
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
     public  class MenuDomainService
    {
        private readonly ISysRepositoryBase<SysMenu> _menuRepository; 
        private readonly SysUnitOfWork _unitOfWork;  
        public MenuDomainService(ISysRepositoryBase<SysMenu> menuRepository, SysUnitOfWork unitOfWork)
        {
            _menuRepository = menuRepository;
            _unitOfWork = unitOfWork;
        }
        public List<SysMenu> GetAllMenuList()
        {
            return _menuRepository.Get(x => x.IsDeleted == (int)CommonEnum.IsDeleted.No && x.Status == (int)CommonEnum.Status.Normal).ToList();
        }
        public ApiResponsePage<SysMenu> GetMenuList(ApiRequestPage<MenuRequestPayload> requserobj)
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
            var query = _menuRepository.DBContext.SysMenu.AsQueryable();
            if (!string.IsNullOrEmpty(requserobj.Kw))
            {
                query = query.Where(x => x.Name.Contains(requserobj.Kw.Trim()) || x.Url.Contains(requserobj.Kw.Trim()));
            }
            if (requserobj.Where.IsDeleted > CommonEnum.IsDeleted.All)
            {
                query = query.Where(x => x.IsDeleted == (int)requserobj.Where.IsDeleted);
            }
            if (requserobj.Where.Status > UserStatus.All)
            {
                query = query.Where(x => x.Status == (int)requserobj.Where.Status);
            }
            if (requserobj.Where.ParentGuid.HasValue)
            {
                query = query.Where(x => x.ParentGuid == requserobj.Where.ParentGuid);
            }
            #endregion
            #region 分页查询
            var dataRequest = new ApiRequestPage<IQueryable<SysMenu>>()
            {
                PageIndex = requserobj.PageIndex,
                PageSize = requserobj.PageSize,
                OrderBy = requserobj.OrderBy,
                SortCol = requserobj.SortCol,
                Where = query
            };
            ApiResponsePage<SysMenu> pageuserlist = _menuRepository.ToPage(dataRequest);
            #endregion
            return pageuserlist;
        }
        public SysMenu GetMenuByKey(Guid menuid)
        {
            var sysmodel = _menuRepository.GetByKey(menuid);
            return sysmodel;
        }
        public void CreateMenu(SysMenu model)
        {
            _menuRepository.Add(model);
            _unitOfWork.SaveChanges();
        }
        public void EditMenu(SysMenu model)
        {
            _menuRepository.Update(model);
            _unitOfWork.SaveChanges();
        }
        public void UpdateIsDeleteMenu(CommonEnum.IsDeleted isDeleted, string ids)
        {
            var parameters = ids.Split(',').Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
            var sql = string.Format("UPDATE Sys_Menu SET IsDeleted=@IsDeleted WHERE id IN ({0})", parameterNames);
            parameters.Add(new SqlParameter("@IsDeleted", (int)isDeleted));
            _menuRepository.ExecuteSql(sql, parameters);
            _unitOfWork.SaveChanges();
        }
        public void UpdateStatus(UserStatus status, string ids)
        {
            var parameters = ids.Split(',').Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
            var sql = string.Format("UPDATE Sys_Menu SET Status=@Status WHERE id IN ({0})", parameterNames);
            parameters.Add(new SqlParameter("@Status", (int)status));
            _menuRepository.ExecuteSql(sql, parameters);
            _unitOfWork.SaveChanges();
        }
    }
}