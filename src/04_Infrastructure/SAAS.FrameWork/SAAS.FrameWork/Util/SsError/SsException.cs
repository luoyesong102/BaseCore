﻿using Newtonsoft.Json.Linq;
using SAAS.FrameWork.Extensions;
using System;

namespace SAAS.FrameWork.Util.SsError
{

    public class SsException :  Exception
    {
        /// <summary>
        /// 可添加自定义事件。如当异常创建时 自动记录日志。
        /// </summary>
        public static Action<SsException> Event_OnCreateException = null;
         

        public SsException()
        {
            try
            {
                Event_OnCreateException?.Invoke(this);
            }
            catch { }
        }



        #region ErrorCode

        public int? ErrorCode
        {
            get
            {
                return this.ErrorCode_Get();
            }
            set
            {
                this.ErrorCode_Set(value);
            }
        }
        #endregion

        #region ErrorMessage

        public string ErrorMessage
        {
            get
            {
                return this.ErrorMessage_Get();
            }
            set
            {
                this.ErrorMessage_Set(value);
            }
        }
        #endregion


        #region ErrorTag

        public string ErrorTag
        {
            get
            {
                return this.ErrorTag_Get();
            }
            set
            {
                this.ErrorTag_Set(value);
            }
        }
        #endregion


        #region ErrorDetail

        public JObject ErrorDetail
        {
            get
            {
                return this.ErrorDetail_Get();
            }
            set
            {
                this.ErrorDetail_Set(value);
            }
        }
        #endregion


    }
}
