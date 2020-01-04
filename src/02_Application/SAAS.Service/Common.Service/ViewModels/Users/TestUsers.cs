using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Common.Service.CommonEnum;

namespace Common.Service.DTOModel
{
    public static class UsersList
    {
        public static List<UserContext> Users = new List<UserContext>
        {
            new UserContext{ Id = Guid.NewGuid(), UserName = "Paul", Password = "Paul123", Roles = new List<string>{ "administrator", "api_access" }, Urls = new List<string>{ "/api/values/getadminvalue", "/api/values/getguestvalue" }},
            new UserContext{ Id = Guid.NewGuid(), UserName = "Young", Password = "Young123", Roles = new List<string>{ "api_access" }, Urls = new List<string>{ "/api/values/getguestvalue" }},
            new UserContext{ Id = Guid.NewGuid(), UserName = "admin", Password = "111111", Roles = new List<string>{ "administrator" }, Urls = new List<string>{ "/api/values/getadminvalue","/api/v1/test/test/list" }},
        };
    }

    public class UserContext
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Urls { get; set; }
    }

   
}
