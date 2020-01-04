
using DemoEF.Domain.Models;
using SAAS.Framework.Orm.EfCore.Repositories;
using System;
using System.Collections.Generic;




namespace DemoEF.Domain
{
    /// <summary>
    /// 领域层：做元数据的仓储操作，元数据的验证
    /// </summary>
    public class AppMomainService 
    {
        private readonly IEFRepository<EFDbContext> _crmRepository;
      
        public AppMomainService(IEFRepository<EFDbContext>  crmRepository)
        {
            _crmRepository = crmRepository;
        }
       
        public string GetStrTest()
        {
            TbAccount appmodelnewtest = _crmRepository.GetByTracking<TbAccount>(p => p.UserId == 1);

            //appmodelnewtest.AppExternalName = "556565656565";
            //appmodelnewtest.FlowId = 2;
            //_crmRepository.UpdateState<App>(appmodelnewtest);
            //var TEST1 = _crmRepository.GetByTracking<App>(U => U.Id == 1018456430824525824);
            //var TEST=     _crmRepository.GetNoTracking<App>(U => U.Id == 1018456430824525824);
            //_crmRepository.SaveChanges();
            //var TEST3 = _crmRepository.GetByTracking<App>(U => U.Id == 1018456430824525824);
            //var TEST4 = _crmRepository.GetNoTracking<App>(U => U.Id == 1018456430824525824);
            //App appmodel = _crmRepository.GetByTracking<App>(p => p.Id == 1018456430824525824);
            //appmodel.AppExternalName = "test1111";
            //appmodel.FlowId = 2;
            //_crmRepository.Update(appmodel);

            //App appmodelnew = _crmRepository.Get<App>(p => p.Id == 1018456430824525824);
            //appmodelnew.Id = _idWorker.NextId();
            //appmodelnew.AppExternalName = "jgl";
            //_crmRepository.Save(appmodelnew);
            // _crmRepository.GetAsync(p => p.Id == 1018456430824525824);
            return "成功返回字符串！";
        }
    }
}
