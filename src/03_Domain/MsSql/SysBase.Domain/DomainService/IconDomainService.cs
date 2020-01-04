using Common.Service;
using Common.Servicei.ViewModels.Users;
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
     public  class IconDomainService
    {
        private readonly ISysRepositoryBase<SysIcon> _iconRepository; 
        private readonly SysUnitOfWork _unitOfWork;  
        public IconDomainService(ISysRepositoryBase<SysIcon> iconRepository, SysUnitOfWork unitOfWork)
        {
            _iconRepository = iconRepository;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="requserobj"></param>
        /// <returns></returns>
        public ApiResponsePage<SysIcon> GetIconList(ApiRequestPage<CommonModel> requserobj)
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
            var query = _iconRepository.DBContext.SysIcon.AsQueryable();
            if (!string.IsNullOrEmpty(requserobj.Kw))
            {
                query = query.Where(x => x.Code.Contains(requserobj.Kw.Trim()) || x.Custom.Contains(requserobj.Kw.Trim()));
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
            var dataRequest = new ApiRequestPage<IQueryable<SysIcon>>()
            {
                PageIndex = requserobj.PageIndex,
                PageSize = requserobj.PageSize,
                OrderBy = requserobj.OrderBy,
                SortCol = requserobj.SortCol,
                Where = query
            };
            ApiResponsePage<SysIcon> pageuserlist = _iconRepository.ToPage(dataRequest);
            #endregion
            return pageuserlist;
        }

        public List<SysIcon> GetIconListByKey(string keys)
        {
            var query = _iconRepository.LoadListAll<SysIcon>(x => x.Code.Contains(keys));

            var list = query.ToList();
            return list;
        }
        public SysIcon GetIconById(int Id)
        {
        return    _iconRepository.GetByKey(Id);
        }
        public bool ExistIcon(string code, int Id)
        {
          return  _iconRepository.IsExist<SysIcon>(u => u.Code == code && u.Id != Id);
        }
        public void CreateIcon(SysIcon model)
        {

            if (_iconRepository.IsExist<SysIcon>(x => x.Code == model.Code))
            {
                throw new BaseException("图标已存在");
            }
            _iconRepository.Add(model);
            _unitOfWork.SaveChanges();
        }
        public void CreateIconList(List<SysIcon> model)
        {
            _iconRepository.AddList(model);
            _unitOfWork.SaveChanges();
        }
        public void UpdateIcon(SysIcon model)
        {
            _iconRepository.Update(model);
            _unitOfWork.SaveChanges();
        }
        public void UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids)
        {
            var parameters = ids.Split(',').Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
            var sql = string.Format("UPDATE Sys_Icon SET IsDeleted=@IsDeleted WHERE Id IN ({0})", parameterNames);
            parameters.Add(new SqlParameter("@IsDeleted", (int)isDeleted));
            _iconRepository.ExecuteSql(sql, parameters);
            _unitOfWork.SaveChanges();
        }
        public void UpdateStatus(UserStatus status, string ids)
        {
            var parameters = ids.Split(',').Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
            var sql = string.Format("UPDATE Sys_Icon SET Status=@Status WHERE Id IN ({0})", parameterNames);
            parameters.Add(new SqlParameter("@Status", (int)status));
            _iconRepository.ExecuteSql(sql, parameters);
            _unitOfWork.SaveChanges();
        }
    }
}