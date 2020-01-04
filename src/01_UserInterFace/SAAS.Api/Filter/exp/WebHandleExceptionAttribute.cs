
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SAAS.FrameWork.Util.Exp;

namespace YH.SAAS.API
{
    /// <summary>
    /// 
    /// </summary>
    public class WebHandleExceptionAttribute:HandleExceptionAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        /// <summary>
        /// 
        /// </summary>
        public WebHandleExceptionAttribute(
            IHostingEnvironment hostingEnvironment,
            IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override bool HandleException(Exception ex)
        {
            //DO something
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetExceptionInfo(Exception ex, bool isLocalRequest)
        {
            if (!(ex is BusinessException))
            {
                HandleException(ex);
            }
            if (isLocalRequest)
            {
                if(ex is BusinessException)
                {
                    return ex.Message;
                }
                return ex.ToString();
            }
            else
            {
                return "系统发生异常，请稍后再试。";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        protected override ActionResult BuildAjaxJsonActionResult(Exception ex, bool isLocalRequest)
        {
            // JsonResult jr = new JsonResult(new { Success = false, Message = GetExceptionInfo(ex, isLocalRequest) });
            JsonResult jr = new JsonResult(ExceptionManager.SetException<string>(ex, GetExceptionInfo(ex, isLocalRequest)));
            return jr;


        }

        /// <summary>
        /// 
        /// </summary>
        protected override ActionResult BuildAjaxHtmlActionResult(Exception ex, bool isLocalRequest)
        {
            string message = GetExceptionInfo(ex, isLocalRequest);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\"MessageCenter_Exception\">");
            sb.AppendFormat("<input id=\"errorMessage\" type=\"hidden\" value=\"{0}\" />", HttpUtility.HtmlEncode(message));
            sb.Append("</div>");
            return new ContentResult
            {
                Content = sb.ToString(),
                ContentType = "text/html"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        protected override ActionResult BuildAjaxXmlActionResult(Exception ex, bool isLocalRequest)
        {
            string message = GetExceptionInfo(ex, isLocalRequest);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\"?>");
            sb.AppendLine("<result>");
            sb.AppendLine("<error>true</error>");
            sb.AppendLine("<message>" + message.Replace("<", "&lt;").Replace(">", "&gt;") + "</message>");
            sb.AppendLine("</result>");
            return new ContentResult
            {
                Content = sb.ToString(),
                ContentType = "application/xml"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        protected override ActionResult BuildWebPageActionResult(Exception ex, bool isLocalRequest, ExceptionContext filterContext)
        {
            string errorStr = GetExceptionInfo(ex, isLocalRequest);
            Exception exception = new Exception(errorStr);

            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();

            HandleErrorInfo model = new HandleErrorInfo
            {
                ControllerName = controller,
                ActionName = action,
                Exception = exception
            };

            var result = new ViewResult { ViewName = "ErrorOccurred" };
            result.ViewData = new ViewDataDictionary(_modelMetadataProvider, filterContext.ModelState);
            result.ViewData.Add("HandleErrorInfo", model);

            return result;
        }
    }
}
