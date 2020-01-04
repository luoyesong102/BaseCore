/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * 版权所有，请勿删除
 ******************************************/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SAAS.FrameWork.Util.Exp;
using System.Linq;

namespace SAAS.Api.Validate
{
    /// <summary>
    /// DB验证类在此收集错误信息
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
   
        /// <summary>
        /// DB字段长度，空值判断等特性在此进行判断
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Result = new ObjectResult(
                    actionContext.ModelState.Values
                        .SelectMany(e => e.Errors)
                        .Select(e => e.ErrorMessage));

                var ErrorList = actionContext.ModelState.Values
                        .SelectMany(e => e.Errors)
                        .Select(e => e.ErrorMessage).ToArray();
               var  ErrorStr=  string.Join(',', ErrorList);
                throw new BaseException(1000, ErrorStr);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Result = new ObjectResult(
                    actionContext.ModelState.Values
                        .SelectMany(e => e.Errors)
                        .Select(e => e.ErrorMessage));

                var ErrorList = actionContext.ModelState.Values
                        .SelectMany(e => e.Errors)
                        .Select(e => e.ErrorMessage).ToArray();
                var ErrorStr = string.Join(',', ErrorList);
                throw new BaseException(1000, ErrorStr);
            }
           
        }

        public override void OnResultExecuting(ResultExecutingContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Result = new ObjectResult(
                    actionContext.ModelState.Values
                        .SelectMany(e => e.Errors)
                        .Select(e => e.ErrorMessage));

                var ErrorList = actionContext.ModelState.Values
                        .SelectMany(e => e.Errors)
                        .Select(e => e.ErrorMessage).ToArray();
                var ErrorStr = string.Join(',', ErrorList);
                throw new BaseException(1000, ErrorStr);
            }
            
        }

        public override void OnResultExecuted(ResultExecutedContext actionContext)
        {

            if (!actionContext.ModelState.IsValid)
            {
               

                var ErrorList = actionContext.ModelState.Values
                        .SelectMany(e => e.Errors)
                        .Select(e => e.ErrorMessage).ToArray();
                var ErrorStr = string.Join(',', ErrorList);
                throw new BaseException(1000, ErrorStr);
            }
            
        }
      

    }
}
