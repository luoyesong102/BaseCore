
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAAS.FrameWork.Util.Exp
{

    // MessageResponse 错误代码 0或者空正常 1服务内部异常 2服务安全校验失败，3业务异常
    //UserValidException 1 表示用户名密码错误，其他表示token无效
    public class ExceptionManager
    {
        public static ApiResponseModel<T> SetException<T>(Exception ex, string message)  
        {
            ApiResponseModel<T> rValue = new ApiResponseModel<T>();
            
            if (ex is BusinessException || ex is ApplicationException)
            {
                BusinessException bEx = ex as BusinessException;
                rValue.Code = ServerErrcodeEnum.ServiceError; 
                rValue.Message = ex.Message;
               
            }
            else
            {
                rValue.Code = ServerErrcodeEnum.ServiceError;//系统异常
                rValue.Message = "服务未处理异常";
              
            }
            return rValue;
        }
        public static ApiResponsePage<T> SetExceptionPage<T>(Exception ex,string message)
        {

            ApiResponsePage<T> rValue = new ApiResponsePage<T>();
            if (ex is BusinessException || ex is ApplicationException)
            {
                BusinessException bEx = ex as BusinessException;
                rValue.Code = ServerErrcodeEnum.ServiceError;
                rValue.Message = message;
              

                //LogTxt.WriteLog("BusinessException" + bEx.ErrorCode, (int)LogType.Other);
            }
            else
            {
                rValue.Code = ServerErrcodeEnum.ServiceError;//系统异常
                rValue.Message = message;
                Exception logException = ex;
                if (ex.InnerException != null)
                    logException = ex.InnerException;
               // LogTxt.WriteLog("BusinessException" + logException.Message, (int)LogType.Other);

            }
            return rValue;
        }

        public static SysResponseModel<T> SetResponseException<T>(Exception ex)
        {

            SysResponseModel<T> rValue = new SysResponseModel<T>();
            if (ex is BaseException || ex is ApplicationException)
            {
                BaseException bEx = ex as BaseException;
                rValue.Header.ReturnCode = bEx.Code;
                rValue.Header.Message = ex.Message;
                rValue.Body = default(T);

              //  LogTxt.WriteLog("BusinessException" + bEx.Code, (int)LogType.Other);
            }
            else
            {
                rValue.Header.ReturnCode =(int) ServerErrcodeEnum.ServiceError;//系统异常
                rValue.Header.Message = "服务未处理异常";
                rValue.Body = default(T);
                Exception logException = ex;
                if (ex.InnerException != null)
                    logException = ex.InnerException;
              //  LogTxt.WriteLog("BusinessException" + logException.Message, (int)LogType.Other);

            }
            return rValue;
        }
        public static SyseResponsePag<T> SetResponseExceptionPage<T>(Exception ex)
        {

            SyseResponsePag<T> rValue = new SyseResponsePag<T>();
            if (ex is BaseException || ex is ApplicationException)
            {
                BaseException bEx = ex as BaseException;
                rValue.Header.ReturnCode = bEx.Code;
                rValue.Header.Message = ex.Message;
             //   LogTxt.WriteLog("BusinessException" + bEx.Code, (int)LogType.Other);
            }
            else
            {
                rValue.Header.ReturnCode = (int)ServerErrcodeEnum.ServiceError;//系统异常
                rValue.Header.Message = "服务未处理异常";
                Exception logException = ex;
                if (ex.InnerException != null)
                    logException = ex.InnerException;
                //LogTxt.WriteLog("BusinessException" + logException.Message, (int)LogType.Other);

            }
            return rValue;
        }
    }
}
