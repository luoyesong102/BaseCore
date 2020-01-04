using IdentityS4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityS4.Services
{
    public interface IAdminService:IBaseService<Admin>
    {
        Task<Admin> GetByStr(string username, string pwd);
    }
}
