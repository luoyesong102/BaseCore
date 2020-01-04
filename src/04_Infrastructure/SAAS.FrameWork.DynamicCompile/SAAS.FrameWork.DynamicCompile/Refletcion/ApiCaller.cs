using SAAS.FrameWork.Attributes;
using SAAS.FrameWork.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SAAS.FrameWork
{
    public class ApiCaller
    {
        public object _instance;
        public ApiCaller(object instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            _instance = instance;
        }

        public T Call<T>(string moudelName, string apiFlag, params object[] @params)
        {
            var method = ReflectionUtils.GetMethod<PortalMoudelAttribute, ApiFlagAttribute>
                 ((a, t) => a.Name == moudelName,
                  api =>api != null && !string.IsNullOrEmpty(api.Name) &&  api.Name.ToLower() == apiFlag.ToLower());

            if (method != null)
            {
                var obj = Activator.CreateInstance(method.ReflectedType);
                return (T)method.Invoke(obj, @params);
            }
            return default(T);
        }
    }
}
//var EditResult = apiCaller.Call<BaseViewModel>("CallPhoneService", "EditUserToGroup", AddUserInfo, entity.GroupIndex, entity.GroupName, "4", "1");
//  [PortalMoudel("CallCenterService")]
//[ApiFlag("GetGroupUserAgeNo")]