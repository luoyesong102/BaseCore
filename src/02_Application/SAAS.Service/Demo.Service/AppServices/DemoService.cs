using Demo.Domain;
using DemoEF.Domain;
using Microsoft.Extensions.Logging;
using SysBase.Domain.DomainService;
using System.Threading.Tasks;

namespace Demo.Service
{
    /// <summary>
    /// 应用层：领域适配:做数据转化，领域的适配（拆分的颗粒度-聚合根或者每一个元数据）
    /// </summary>
    public class DemoService : IDemoService
    {
        readonly DemoMomainService h5cussmapi;
        readonly AppMomainService appservice;
        readonly UserDomainService userservice;
        public DemoService(DemoMomainService _h5cussmapi, AppMomainService _appservice, UserDomainService _userservice)
        {
            h5cussmapi = _h5cussmapi;
            appservice = _appservice;
            userservice = _userservice;
        }
        public  string GetStrTest()
        {
            //h5cussmapi.GetStrTest();
            //appservice.GetStrTest();
         var listuser=    userservice.GetUserList();
            return listuser[0].Id.ToString();
        }


    }
}
