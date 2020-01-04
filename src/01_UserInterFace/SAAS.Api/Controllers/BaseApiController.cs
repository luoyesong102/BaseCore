using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAAS.Api.Router;

namespace Controllers
{
    /// <summary>
    /// 基础类，带自定义路由版本控制的
    /// </summary>
    [CustomRoute]
    [ApiController]
    [Produces("application/json")]
    public class BaseApiController : ControllerBase
    {
        
    }
}
