
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

    public class IconService : IIconService
    {
       
        readonly IconDomainService iconservice;
        public IconService( IconDomainService _iconservice)
        {
          
            iconservice = _iconservice;
        }
        public ApiResponsePage<SysIcon> GetIconList(ApiRequestPage<CommonModel> requserobj)
        {
            return iconservice.GetIconList(requserobj);
        }
        public List<SysIcon> GetIconListByKey(string keys)
        {
            return iconservice.GetIconListByKey(keys);
        }
        public void CreateIconList(List<SysIcon> model)
        {
            iconservice.CreateIconList(model);
        }
        public  SysIcon GetIconById(int Id)
        {
          return  iconservice.GetIconById(Id);
        }
        public bool ExistIcon(string code, int Id)
        {
          return  iconservice.ExistIcon(code, Id);
        }
        public void CreateIcon(SysIcon model)
        {
            iconservice.CreateIcon(model);
        }
        public void UpdateIcon(SysIcon model)
        {
            iconservice.UpdateIcon(model);
        }
        public void UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids)
        {
            iconservice.UpdateIsDelete(isDeleted, ids);
        }
        public void UpdateStatus(UserStatus status, string ids)
        {
            iconservice.UpdateStatus(status, ids);
        }
    }
}
