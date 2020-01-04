﻿using Newtonsoft.Json.Linq;
using SAAS.FrameWork.Util.SsError;
using System;

namespace SAAS.FrameWork.Extensions
{
    public static partial class ExceptionExtensions 
    {
        #region Data


        public static void Data_Set<Type>(this Exception ex, string key,Type data)
        {
            if (ex == null)
            {              
                throw new ArgumentNullException(nameof(ex));
            }
            ex.Data[key] = data;
        }


        public static Type Data_Get<Type>(this Exception ex, string key)
        {
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex));
            }
            return (ex.Data[key]).Convert<Type>() ;
        }
        #endregion


        #region ErrorCode

        public static Exception ErrorCode_Set(this Exception ex,int? ErrorCode)
        {
            ex.Data_Set("ErrorCode", ErrorCode);
            return ex;
        }

        public static int? ErrorCode_Get(this Exception ex)
        {
            return ex.Data_Get<int?>("ErrorCode");
        }
        #endregion


        #region ErrorMessage

        public static Exception ErrorMessage_Set(this Exception ex, string ErrorMessage)
        {
            ex.Data_Set("ErrorMessage", ErrorMessage);
            return ex;
        }

        public static string ErrorMessage_Get(this Exception ex)
        {
            return ex.Data_Get<string>("ErrorMessage")??ex.Message;
        }
        #endregion


        #region ErrorTag
        /// <summary>
        /// 自定义ErrorTag格式。每处ErrorTag建议唯一。建议格式为 日期_作者缩写_自定义序号，例如："150721_lith_1"
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ErrorTag"></param>
        public static Exception ErrorTag_Set(this Exception ex, string ErrorTag)
        {
            ex.Data_Set("ErrorTag", ErrorTag);
            return ex;
        }

        /// <summary>
        /// 自定义ErrorTag格式。每处ErrorTag建议唯一。建议格式为 日期_作者缩写_自定义序号，例如："150721_lith_1"
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string ErrorTag_Get(this Exception ex)
        {
            return ex.Data_Get<string>("ErrorTag");
        }
        #endregion


        #region ErrorDetail
        /// <summary>
        /// 设置ErrorDetail
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ErrorDetail"></param>
        public static Exception ErrorDetail_Set(this Exception ex, JObject ErrorDetail)
        {
            ex.Data_Set("ErrorDetail", ErrorDetail);
            return ex;
        }
        /// <summary>
        /// 获取ErrorDetail，可能为null
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static JObject ErrorDetail_Get(this Exception ex)
        {
            return ex.Data_Get<JObject>("ErrorDetail");
        }
        #endregion

        #region ErrorDetail
        /// <summary>
        /// 设置ErrorDetail的属性值。若ErrorDetail为null,则会自动创建
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>

        public static Exception ErrorDetail_Set(this Exception ex, string key,object value)
        {
            var ErrorDetail = ex.ErrorDetail_Get();
            if (null == ErrorDetail)
            {
                ex.ErrorDetail_Set(ErrorDetail = new JObject());
            }
            ErrorDetail[key] = value.ToJToken();
            return ex;

        }

        /// <summary>
        /// 获取ErrorDetail的属性值
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object ErrorDetail_Get(this Exception ex, string key)
        {
            return ex.ErrorDetail_Get()?[key]?.GetValue();
        }
        #endregion



        #region SsError
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>

        public static Exception SsError_Set(this Exception ex, SsError ssError)
        {            
            return ssError.SetErrorToException(ex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static SsError SsError_Get(this Exception ex)
        {
            return new SsError().LoadFromException(ex);
        }
        #endregion


    }
}
