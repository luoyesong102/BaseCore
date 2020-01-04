﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAAS.FrameWork
{
    public class SysRequesModel<TModel>
    {

        public RequestHeader Header { get; set; }

        /// <summary>
        /// 对象
        /// </summary>
        public TModel Body { get; set; }

        public SysRequesModel()
        {
            Header = new RequestHeader();
        }


    }


    public class RequestHeader
    {

        public string RequestType { get; set; }
        /// <summary>
        /// 运用程序ID
        /// </summary>
        public int AppID { get; set; }

        /// <summary>
        /// 每次访问 生成的GUID,查日记的时候，可以按照GUID去查询
        /// </summary>
        public string GUID { get; set; }

    }


    public class SysResponseModel
    {
        public ResponseHeader Header { get; set; }

        public SysResponseModel()
        {
            Header = new ResponseHeader();
        }
    }


    public class SysResponseModel<TModel> : SysResponseModel
    {


        public TModel Body { get; set; }


    }

    public class ResponseHeader
    {
        /// <summary>
        /// 返回错误码，0无错，-1 客户端错误
        /// </summary>
        public int ReturnCode { get; set; }

        /// <summary>
        /// 页面属性的名字
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
    }
}
