/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * 版权所有，请勿删除
 ******************************************/

using Microsoft.AspNetCore.Http;
using SAAS.FrameWork.Extensions;
using SAAS.FrameWork.Module.Api.Data;
using SAAS.FrameWork.Util.Exp;
using SAAS.FrameWork.Util.SsError;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ExceptionService
{
    /// <summary>
    /// 异常中间件  app.ConfigureCustomExceptionMiddleware();
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorreturn = new ApiReturn() { success = false };
            if (exception is BaseException)
            {
               var busexception = exception as BaseException;
                errorreturn.error = new SsError { errorCode = 1000, errorMessage = busexception.Message };
            }
             else
            {
                errorreturn.error = new SsError { errorCode = -1, errorMessage = $"资源服务器忙,请稍候再试,原因:{exception.Message}" };
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 200;//抛弃http标准定义异常编码，统一http的状态，系统来进行异常的分类

            return context.Response.WriteAsync(errorreturn.Serialize());
        }
    }
}
