using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SAAS.Api.Router;


namespace SAAS.Api.Controllers.V2
{


    /// <summary>
    /// 测试接口V2
    /// </summary>
     [ApiVersion("2.0")]
    [CustomRoute(ApiVersions.v2, "test")]
    public class TestController : BaseApiController
    {
      
        
        /// <summary>
        /// 列表页请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public IActionResult GetList()
        {
            return Ok(new { version = "list-v2" });
        }
        /// <summary>
        /// 详情页请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("detail")]
        public IActionResult GetDetail()
        {
            return Ok(new { version = "detail-v2" });
        }
    }
}