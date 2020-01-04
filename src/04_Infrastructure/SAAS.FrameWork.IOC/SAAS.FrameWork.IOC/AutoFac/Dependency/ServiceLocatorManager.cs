using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAAS.Dependency
{
    public class ServiceLocatorManager
    {
        private static IServiceProvider _serviceProvider;

        public static void Configure(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static object Resolve(Type t)
        {
            if (_serviceProvider == null)
                return null;

            return _serviceProvider.GetService(t);
        }

        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
    }
}
