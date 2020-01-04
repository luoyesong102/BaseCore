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
    /// 上传接口[FromServices]IHostingEnvironment environment
    /// </summary>
    [ApiVersion("1.0")]
    [CustomRoute(ApiVersions.v1, "upload")]
    public class UpLoadController : BaseApiController
    {
        private readonly IHostingEnvironment _environment;
        private readonly IDemoService _demoservice;
      
        public UpLoadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("insertpicture")]
        public async Task<bool> InsertPicture()
        {
            var data = new PicData();
            string path = string.Empty;
            var files = Request.Form.Files;
            if (files == null || files.Count() <= 0) { data.Msg = "请选择上传的文件。"; return false; }
            //格式限制
            var allowType = new string[] { "image/jpg", "image/png", "image/jpeg" };
            if (files.Any(c => allowType.Contains(c.ContentType)))
            {
                if (files.Sum(c => c.Length) <= 1024 * 1024 * 4)
                {
                    foreach (var file in files)
                    {
                        string strpath = Path.Combine("Upload", DateTime.Now.ToString("MMddHHmmss") + file.FileName);
                        path = Path.Combine(_environment.WebRootPath, strpath);

                        using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    data.Msg = "上传成功";
                    return true;
                }
                else
                {
                    data.Msg = "图片过大";
                    return false;
                }
            }
            else

            {
                data.Msg = "图片格式错误";
                return false;
            }
        }
    }
}