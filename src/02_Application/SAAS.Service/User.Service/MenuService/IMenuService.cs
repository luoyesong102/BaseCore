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
using Common.Service.DTOModel.Menu;

namespace Menu.Service
{
    public interface IMenuService : AutoInject
    {
        List<SysMenu> GetAllMenuList();
        ApiResponsePage<SysMenu> GetMenuList(ApiRequestPage<MenuRequestPayload> requserobj);
        SysMenu GetMenuByKey(Guid menuid);
        void CreateMenu(SysMenu model);
        void EditMenu(SysMenu model);
        void UpdateIsDeleteMenu(CommonEnum.IsDeleted isDeleted, string ids);
        void UpdateStatus(UserStatus status, string ids);
    }

  
}
