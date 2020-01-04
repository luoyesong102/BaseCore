using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controllers;
using Demo.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SAAS.Api.Router;
using SAAS.FrameWork.Module.Api.Data;


namespace SAAS.Api.Controllers.V1
{

    /// <summary>
    /// 测试接口
    /// </summary>
     [ApiVersion("1.0")]
    [CustomRoute(ApiVersions.v1, "test")]
    public class TestController : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IDemoService _demoservice;
      
        public TestController(IConfiguration configuration, IDemoService demoservice)
        {
            _configuration = configuration;
            _demoservice = demoservice;
        }

        /// <summary>
        /// 列表页请求
        /// </summary>
        /// <returns></returns>
       // [Authorize(Policy = "Permission")]
        //[Authorize(Policy = "Administrator")]
        [Authorize(Roles  = "管理员")]
        [HttpGet]
        [Route("list")]
        public IActionResult GetList()
        {

            return Ok(new { version = "list-v1" });
            //return BadRequest("test");
        }
        /// <summary>
        /// 详情页请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("detail")]
        public ApiReturn<object>  GetDetail()
        {
            ApiReturn<object> apiobj = new ApiReturn<object>();
            apiobj.success = true;
             string str=  _demoservice.GetStrTest();//ApiReturn<PageData<Role1OrderListModel>> Opt11(PageInfo pager, DataFilter[] filter, SortItem[] sort)
            var test = new { age = 3, name = str };
            apiobj.data = test;
            return apiobj;
        }
        /// <summary>
        /// 修改接口
        /// </summary>
        /// <param name="value"></param>
        [Route("update")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
            // string str=  _demoservice.GetStrTest();
            //return Ok(new { version = "detail-v1" });
            //return BadRequest("test");
        }
    }
}