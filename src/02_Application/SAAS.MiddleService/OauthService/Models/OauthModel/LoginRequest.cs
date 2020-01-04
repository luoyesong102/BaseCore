using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OauthService.OauthModel
{
    public class LoginRequest
    {
       
        public string UserName { get; set; }
      
      
        public string PassWord { get; set; }
    }
}
