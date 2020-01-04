using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Servicei.ViewModels.Users;
using Controllers;
using Demo.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SAAS.Api.Router;
using SAAS.FrameWork.Module.Api.Data;


namespace SAAS.Api.Controllers.V1
{

    /// <summary>
    /// EXCEL操作
    /// </summary>
    [ApiVersion("1.0")]
    [CustomRoute(ApiVersions.v1, "excel")]
    public class ExcelController : BaseApiController
    {
        private readonly IHostingEnvironment _environment;
        private readonly IDemoService _demoservice;
      
        public ExcelController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
       
    }
}