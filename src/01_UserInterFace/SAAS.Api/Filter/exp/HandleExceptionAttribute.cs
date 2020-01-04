using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YH.SAAS.API
{
    /// <summary>
    /// MVC特性异常拦截,目前是弃用，采用拦截http中间件来进行处理 // options.Filters.Add(typeof(WebHandleExceptionAttribute)); // 异常处理
    /// </summary>
    public abstract class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public HandleExceptionAttribute()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected abstract bool HandleException(Exception ex);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="isLocalRequest"></param>
        /// <returns></returns>
        protected abstract ActionResult BuildAjaxJsonActionResult(Exception ex, bool isLocalRequest);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="isLocalRequest"></param>
        /// <returns></returns>
        protected abstract ActionResult BuildAjaxHtmlActionResult(Exception ex, bool isLocalRequest);

        /// <summary>
        /// 
        /// </summary>
        protected abstract ActionResult BuildAjaxXmlActionResult(Exception ex, bool isLocalRequest);

        /// <summary>
        /// 
        /// </summary>
        protected abstract ActionResult BuildWebPageActionResult(Exception ex, bool isLocalRequest, ExceptionContext filterContext);

        /// <summary>
        /// 
        /// </summary>
        protected virtual ActionResult BuildResult(Exception ex, ExceptionContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var headers = request.GetTypedHeaders();
            bool isAjaxRequest = headers.Headers.TryGetValue("X-Requested-With", out var header) && header.ToString().ToLower() == "XMLHttpRequest".ToLower();
            bool isLocalRequest = request.Host.Host.ToLower().Contains("localhost") || request.Host.Host.Contains("127.0.0.1");
            ActionResult result;
            if (true)
            {
                var acceptTypes = from at in headers.Accept select at.MediaType.ToString().ToLower();
                if (acceptTypes.Contains("application/json"))
                {
                    result = this.BuildAjaxJsonActionResult(ex,isLocalRequest);
                }
                else if (acceptTypes.Contains("text/html"))
                {
                    result = this.BuildAjaxHtmlActionResult(ex, isLocalRequest);
                }
                else
                {
                    result = this.BuildAjaxXmlActionResult(ex, isLocalRequest);
                }
            }
            else
            {
                result = this.BuildWebPageActionResult(ex, isLocalRequest, filterContext);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                // TODO: Pass additional detailed data via ViewData
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.StatusCode = 200;
                context.ExceptionHandled = true; // mark exception as handled
                context.HttpContext.Response.Headers.Append("Access-Control-Allow-Headers", "x-requested-with,content-type");

               
                context.Result = this.BuildResult(context.Exception, context);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HandleErrorInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Exception Exception { get; set; }
    }
}
