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
    public interface IIconService : AutoInject
    {
        ApiResponsePage<SysIcon> GetIconList(ApiRequestPage<CommonModel> requserobj);
        List<SysIcon> GetIconListByKey(string keys);
        SysIcon GetIconById(int Id);
        bool ExistIcon(string code, int Id);
        void CreateIcon(SysIcon model);
        void CreateIconList(List<SysIcon> model);
        void UpdateIcon(SysIcon model);
        void UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids);
        void UpdateStatus(UserStatus status, string ids);
    }

  
}
