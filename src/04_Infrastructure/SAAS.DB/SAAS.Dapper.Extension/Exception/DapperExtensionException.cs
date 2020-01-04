using System;

namespace SAAS.Dapper.Extension.Exception
{
    public class DapperExtensionException : ApplicationException
    {
        public DapperExtensionException(string msg) : base(msg)
        {

        }
    }
}
