using System;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using Newtonsoft.Json.Linq;
using SAAS.FrameWork.Extensions;
using SAAS.FrameWork.Log;
using SAAS.FrameWork.Module.Api.Data;
using SAAS.FrameWork.Module.SsApiDiscovery.ApiDesc.Attribute;

namespace SAAS.FrameWork.Util.Common
{
    public class AliSmsApi
    {

        /// <summary>
        /// https://mp.weixin.qq.com/s/q3vi8QPR0mmoIXijWf4CWw  excel导入导出
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="smsServiceKey">后台配置中的smsServiceKey</param>
        /// <param name="PhoneNumbers"></param>
        /// <param name="TemplateParam"></param>
        public static void SendSms(ApiReturn<AliSmsRet> ret,string smsServiceKey, string PhoneNumbers,JObject TemplateParam=null)
        {
            var item =ConfigurationManager.ConfigurationManager.Instance.GetByPath<AliSmsReq>("PhoneService.AliSmsServiceConfig." + smsServiceKey);

            if (null == item)
            {
                ret.success = false;
                ret.error = new Util.SsError.SsError { errorCode=2000,errorMessage= "smsServiceKey参数指定的配置不存在" };
                return;
            }

            item.PhoneNumbers = PhoneNumbers;
            item.TemplateParam = TemplateParam?.ToString();

            SendSms(ret, item);
        }


        #region ori api
   

        static void SendSms(ApiReturn<AliSmsRet> ret, AliSmsReq req)
        {
            string strRet = null;
            try
            {
                IClientProfile profile = DefaultProfile.GetProfile("default", req.AccessKeyId, req.AccessKeySecret);
                DefaultAcsClient client = new DefaultAcsClient(profile);
                CommonRequest request = new CommonRequest();
                request.Method = MethodType.POST;
                request.Domain = "dysmsapi.aliyuncs.com";
                request.Version = "2017-05-25";
                request.Action = "SendSms";
                // request.Protocol = ProtocolType.HTTP;
                request.AddQueryParameters("PhoneNumbers", req.PhoneNumbers);
                request.AddQueryParameters("SignName", req.SignName);
                request.AddQueryParameters("TemplateCode", req.TemplateCode);

                if (!string.IsNullOrEmpty(req.TemplateParam))
                {
                    request.AddQueryParameters("TemplateParam", req.TemplateParam);
                }

                CommonResponse response = client.GetCommonResponse(request);

                strRet = System.Text.Encoding.Default.GetString(response.HttpResponse.Content);

                var serviceRet = strRet.Deserialize<AliSmsRet>();              

                if (null!= serviceRet && serviceRet.Code == "OK")
                {
                    ret.data = serviceRet;
                    ret.success = true;
                    return;
                }
                else
                {
                    ret.success = false;
                    ret.error = new Util.SsError.SsError { errorCode = 1000, errorMessage = serviceRet.Message, errorDetail = serviceRet.ConvertBySerialize<JObject>() };
                }

            }

            catch (Exception ex)
            {
                ret.success = false;
                ret.error = ex;
                Logger.log.Error(ex);
            }             
            finally
            {
                var arg = new { req.PhoneNumbers, req.SignName, req.TemplateCode , req.TemplateParam };
                string log = "调用阿里短信服务发送短信。Http请求，request:" + arg.Serialize();
                if (null != strRet)
                {
                    log += Environment.NewLine + "reply:" + strRet;
                }
                Logger.log.Log(Level.ApiTrace,log);
            }
        }


        #region model



        public class AliSmsReq
        {

            /// <summary>
            /// 主账号AccessKey的ID。
            /// </summary>
            [SsExample("LTAIP00vvvvvvvvv")]
            public string AccessKeyId;

            /// <summary>
            /// 
            /// </summary>
            public string AccessKeySecret;


            /// <summary>
            /// 短信签名名称。请在控制台签名管理页面签名名称一列查看。
            /// </summary>
            public string SignName;


            /// <summary>
            /// 短信模板ID。请在控制台模板管理页面模板CODE一列查看。
            /// </summary>
            [SsExample("SMS_153055065")]
            public string TemplateCode;


            /// <summary>
            /// 接收短信的手机号码。支持对多个手机号码发送短信，手机号码之间以英文逗号（,）分隔。上限为1000个手机号码。批量调用相对于单条调用及时性稍有延迟。
            /// </summary>
            [SsExample("15900000000")]
            public string PhoneNumbers;


            /// <summary>
            /// 短信模板变量对应的实际值，JSON格式。
            /// </summary>
            [SsExample("{\"code\":\"1111\"}")]
            public string TemplateParam;

        }


        public class AliSmsRet
        {
            /// <summary>
            /// 发送回执ID，可根据该ID在接口QuerySendDetails中查询具体的发送状态。
            /// </summary>
            [SsExample("90061974693649844")]
            public string BizId;


            /// <summary>
            /// 请求状态码。返回OK代表请求成功。其他错误码详见错误码列表。
            /// </summary>
            [SsExample("OK")]
            public string Code;


            /// <summary>
            /// 状态码的描述
            /// </summary>
            [SsExample("OK")]
            public string Message;


            /// <summary>
            /// 请求ID。
            /// </summary>
            [SsExample("F655A8D5-B967-440B-8683-DAD6FF8DE990")]
            public string RequestId;
        }


        #endregion
        #endregion







    }
}
