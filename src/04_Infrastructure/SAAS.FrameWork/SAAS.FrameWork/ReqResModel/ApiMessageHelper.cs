

namespace SAAS.FrameWork
{
    public class ApiMessageHelper
    {
        /// <summary>
        /// 服务请求对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ApiRequestModel<T> SetRequest<T>(T obj)
        {
            ApiRequestModel<T> mr = new ApiRequestModel<T>();
            mr.Data = obj;
            return mr;
        }
        /// <summary>
        /// 服务返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ApiResponseModel<T> SetResponse<T>(T obj)
        {
            ApiResponseModel<T> mr = new ApiResponseModel<T>();
            mr.Code = ServerErrcodeEnum.Normal;
            mr.Data = obj;
            return mr;
        }
        /// <summary>
        /// 服务分页请求对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ApiRequestPage<T> SetRequestPage<T>(T obj)
        {
            ApiRequestPage<T> mr = new ApiRequestPage<T>();

            return mr;
        }
        /// <summary>
        /// 服务分页返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        
        public static ApiResponsePage<T> SetResponsePage<T>(T obj)
        {
            ApiResponsePage<T> mr = new ApiResponsePage<T>();
            mr.Code = ServerErrcodeEnum.Normal;
            mr.Message = "";
          
            return mr;
        }

     
    }
}