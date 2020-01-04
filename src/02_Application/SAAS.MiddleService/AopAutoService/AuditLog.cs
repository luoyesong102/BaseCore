
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace AopAutoService
{
    /// <summary>
    /// 跟踪审计日志 Autofac.Extras.DynamicProxy.dll
    /// https://www.cnblogs.com/hzzxq/archive/2017/08/16/7373628.html Autofac 之 基于 Castle DynamicProxy 拦截仓储类监控数据库操作
    /// </summary>
    public class AuditLog : IInterceptor
    {
        public AuditLog()
        {
          
        }
        public void Intercept(IInvocation invocation)
        {
          
            string name = invocation.Method.Name;
          
            if (name == "Update"|| name == "Save"||name=="Delete"||name== "UpdateNew"||name== "SaveChanges")
            {
                try
                {
                   

                    invocation.Proceed();
                    //将list写入表
                    //。。。。
                }
                catch (Exception ex)
                {
                   
                }
                finally
                {

                }
            }
            else
            {
                invocation.Proceed();
            }
        }
        public IEnumerable<KeyValuePair<string, object>> MapParameters(object[] arguments, ParameterInfo[] getParameters)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                yield return new KeyValuePair<string, object>(getParameters[i].Name, arguments[i]);
            }
        }

    }
}


//1 builder.RegisterGeneric(typeof(EFRepository<>)).As(typeof(IEFRepository<>)).InstancePerDependency().EnableInterfaceInterceptors().InterceptedBy(typeof(AuditLog));
//2 [AuditLog]