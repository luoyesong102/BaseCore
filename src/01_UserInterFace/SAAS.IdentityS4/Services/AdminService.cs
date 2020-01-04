using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityS4.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityS4.Services
{
    public class AdminService: BaseService<Admin>, IAdminService
    {
        public AdminService(EFContext _efContext)
        {
            db = _efContext;
        }

        public async Task<Admin> GetByStr(string username, string pwd)
        {
            Admin m=await db.Admin.Where(a => a.UserName == username && a.Password == pwd).SingleOrDefaultAsync();
            if (m!=null)
            {
                return m;
            }
            else
            {
                return null;
            }
        }
    }
}
