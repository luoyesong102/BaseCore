using System;
using System.Collections.Generic;

namespace DemoEF.Domain.Models
{
    public partial class TbAccount
    {
        public long UserId { get; set; }
        public string Account { get; set; }
        public string Pwd { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int? AccountState { get; set; }
        public string Email { get; set; }
        public string Tels { get; set; }
        public string Addr { get; set; }
        public string CompanyLevel { get; set; }
        public DateTime? RegistTime { get; set; }
        public string DecoratorReceiveArea { get; set; }
        public int DecoratorLevel { get; set; }
        public string DecoratorTags { get; set; }
        public string Oldpwd { get; set; }
        public string Openid { get; set; }
        public string AccountConfig { get; set; }
    }
}
