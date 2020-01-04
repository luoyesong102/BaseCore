using Aliyun.OSS;
using Aliyun.OSS.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SAAS.FrameWork.Util.Common
{
  public  class OssApi
    {

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="accessKeyId">开发者秘钥对，通过阿里云控制台的秘钥管理页面创建与管理</param>
        /// <param name="accessKeySecret">开发者秘钥对，通过阿里云控制台的秘钥管理页面创建与管理</param>
        /// <param name="endpoint">Endpoint，创建Bucket时对应的Endpoint</param>
        /// <param name="bucketName">Bucket名称，创建Bucket时对应的Bucket名称</param>
        /// <param name="key">文件标识</param>
        /// <param name="file">需要上传文件的文件路径</param>
        public static void PutObject(string accessKeyId, string accessKeySecret, string endpoint, string bucketName, string key, string file)
        {
            var client = new OssClient(endpoint, accessKeyId, accessKeySecret);
            try
            {
                client.PutObject(bucketName, key, file);
            }
            catch (OssException ex)
            {
                Log.Logger.Info($"Msg:{ex.Message};Code:{ex.ErrorCode};RequestID:{ex.RequestId};HostID:{ex.HostId}");
              
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="accessKeyId">开发者秘钥对，通过阿里云控制台的秘钥管理页面创建与管理</param>
        /// <param name="accessKeySecret">开发者秘钥对，通过阿里云控制台的秘钥管理页面创建与管理</param>
        /// <param name="endpoint">Endpoint，创建Bucket时对应的Endpoint</param>
        /// <param name="bucketName">Bucket名称，创建Bucket时对应的Bucket名称</param>
        /// <param name="key">文件标识</param>
        /// <param name="file">下载存放的文件路径</param>
        public static void GetObject(string accessKeyId, string accessKeySecret, string endpoint, string bucketName, string key, string file)
        {
            var client = new OssClient(endpoint, accessKeyId, accessKeySecret);
            try
            {
                var result = client.GetObject(bucketName, key);
                using (var requestStream = result.Content)
                {
                    using (var fs = File.Open(file, FileMode.OpenOrCreate))
                    {
                        int length = 4 * 1024;
                        var buf = new byte[length];
                        do
                        {
                            length = requestStream.Read(buf, 0, length);
                            fs.Write(buf, 0, length);
                        } while (length != 0);
                    }
                }
            }
            catch (OssException ex)
            {
                Log.Logger.Info($"Msg:{ex.Message};Code:{ex.ErrorCode};RequestID:{ex.RequestId};HostID:{ex.HostId}");
               
            }
        }

        /// <summary>
        /// 获取图片地址
        /// </summary>
        /// <param name="accessKeyId">开发者秘钥对，通过阿里云控制台的秘钥管理页面创建与管理</param>
        /// <param name="accessKeySecret">开发者秘钥对，通过阿里云控制台的秘钥管理页面创建与管理</param>
        /// <param name="endpoint">Endpoint，创建Bucket时对应的Endpoint</param>
        /// <param name="bucketName">Bucket名称，创建Bucket时对应的Bucket名称</param>
        /// <param name="key">文件标识</param>
        /// <param name="width">设置图片的宽度</param>
        /// <param name="height">设置图片的高度</param>
        /// <returns></returns>
        public static string GetIamgeUri(string accessKeyId, string accessKeySecret, string endpoint, string bucketName, string key, float width = 100, float height = 100)
        {
            var client = new OssClient(endpoint, accessKeyId, accessKeySecret);
            try
            {
                var process = $"image/resize,m_fixed,w_{width},h_{height}";
                var req = new GeneratePresignedUriRequest(bucketName, key, SignHttpMethod.Get)
                {
                    Expiration = DateTime.Now.AddHours(1),
                    Process = process
                };
                var uri = client.GeneratePresignedUri(req);
                return uri.ToString();
            }
            catch (OssException ex)
            {
                Log.Logger.Info($"Msg:{ex.Message};Code:{ex.ErrorCode};RequestID:{ex.RequestId};HostID:{ex.HostId}");
                return "";
            }
        }
    }

  
}

//var accessKeyId = "";
//var accessKeySecret = "";
//var endpoint = "oss-cn-shenzhen.aliyuncs.com";
//var bucketName = "service-img";

//// 上传文件
//OssApi.PutObject(accessKeyId, accessKeySecret, endpoint, bucketName, "2019012215062801", "C:/Users/as/Desktop/2019012215062801.png");

//// 下载文件
//OssApi.GetObject(accessKeyId, accessKeySecret, endpoint, bucketName, "2019012215062801", "C:/Users/as/Desktop/down.png");

//// 获取图片链接地址
//OssApi.GetIamgeUri(accessKeyId, accessKeySecret, endpoint, bucketName, "2019012215062801", 100, 100);