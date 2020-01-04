
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
using Common.Service.DTOModel.Menu;

namespace Menu.Service
{
   /// <summary>
   /// 1:领域协调
   /// 2：跨领域协调
   /// 3：赋值转化为各自领域对象
   /// 4：特殊的如查询和批量可以不准守
   /// </summary>

    public class MenuService : IMenuService
    {
        readonly MenuDomainService menuservice;
        public MenuService(MenuDomainService _menuservice)
        {

            menuservice = _menuservice;

        }
        public  ApiResponsePage<SysMenu> GetMenuList(ApiRequestPage<MenuRequestPayload> requserobj)
        {
            return menuservice.GetMenuList(requserobj);
        }
        public List<SysMenu> GetAllMenuList()
        {
            return menuservice.GetAllMenuList();
        }
        public SysMenu GetMenuByKey(Guid menuid)
        {
            return menuservice.GetMenuByKey(menuid);
        }
        public void CreateMenu(SysMenu model)
        {
            menuservice.CreateMenu(model);
        }
        public void EditMenu(SysMenu model)
        {
            menuservice.EditMenu(model);
        }
        public void UpdateIsDeleteMenu(CommonEnum.IsDeleted isDeleted, string ids)
        {
            menuservice.UpdateIsDeleteMenu(isDeleted, ids);
        }
        public void UpdateStatus(UserStatus status, string ids)
        {
            menuservice.UpdateStatus(status, ids);
        }
    }
}
