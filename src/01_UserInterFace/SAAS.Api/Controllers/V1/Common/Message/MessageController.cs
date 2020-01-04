/******************************************
 * AUTHOR:          Rector
 * CREATEDON:       2018-09-26
 * OFFICIAL_SITE:    码友网(https://codedefault.com)--专注.NET/.NET Core
 * 版权所有，请勿删除
 ******************************************/

using Controllers;
using Microsoft.AspNetCore.Mvc;
using SAAS.Api.Router;
using SAAS.FrameWork.Module.Api.Data;
using System;

namespace SAAS.Api
{
    /// <summary>
    /// 消息
    /// </summary>
    [ApiVersion("1.0")]
    [CustomRoute(ApiVersions.v1, "messasge")]
    public class MessageController :  BaseApiController
    {
        /// <summary>
        /// 消息总条数
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Count")]
        public ApiReturn<int> Count()
        {
            return 1;
        }

        /// <summary>
        /// 初始化消息标题列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Init")]
        public ApiReturn<object> Init()
        {

            var unread = new object[] {
                new {title="消息1",create_time=DateTime.Now,msg_id=1}
            };
            return new { unread };
        }

        /// <summary>
        /// 获取指定ID的消息内容
        /// </summary>
        /// <param name="msgid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Content/{msgid:int}")]
        public ApiReturn<string> Content(int msgid)
        {
            return $"消息[{msgid}]内容";
        }

        /// <summary>
        /// 将消息标为已读
        /// </summary>
        /// <param name="msgid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("HasRead/{msgid}")]
        public ApiReturn HasRead(int msgid)
        {
            return true;
        }

        /// <summary>
        ///  删除已读消息
        /// </summary>
        /// <param name="msgid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("RemoveRead/{msgid}")]
        public ApiReturn RemoveRead(int msgid)
        {
            return true;
        }

        /// <summary>
        /// 恢复已删消息
        /// </summary>
        /// <param name="msgid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Restore/{msgid}")]
        public ApiReturn Restore(int msgid)
        {
            return true;
        }
    }
}