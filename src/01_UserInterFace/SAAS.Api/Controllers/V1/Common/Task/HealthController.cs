using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskService.Attr;
using TaskService.Utility;
using System;
using System.Net;
using System.Text;
using SAAS.FrameWork.Module.Api.Data;
using SAAS.Api.Router;
using Controllers;

namespace SAAS.Api
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [ApiVersion("1.0")]
    [CustomRoute(ApiVersions.v1, "health")]
    public class HealthController : BaseApiController
    {
        private IHttpContextAccessor httpContextAccessor;
        public HealthController(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }
        /// <summary>
        /// 定时调用此接口让站点一直保持运行状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("keepAlive")]
        public ApiReturn<object> KeepAlive()
        {
            return new { status = true };
        }


        [HttpPost]
        [Route("startindex")]
        public ApiReturn<object> StartIndex()
        {
            return new { status = true, msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")};
        }

        [HttpPost]
        [Route("log")]
        public ApiReturn<object> Log()
        {
          return  new ContentResult()
            {
                Content = FileQuartz.GetAccessLog(1),
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}