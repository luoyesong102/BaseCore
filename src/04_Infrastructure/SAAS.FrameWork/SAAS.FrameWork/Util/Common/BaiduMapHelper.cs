using FrameWork.Net;
using SAAS.FrameWork.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SAAS.FrameWork.Util.Common
{
  
    public static class BaiduMapHelper
    {

        #region 常量
        //百度地图Api  Ak
        public const string BaiduAk = "ttGrBfk4Fv4lFz2LVgA8uz24yPudsthG";

        /// <summary>
        /// 经纬度  逆地理编码 Url  需要Format 0.ak  1.经度  2.纬度
        /// </summary>
        private const string BaiduGeoCoding_ApiUrl = "http://api.map.baidu.com/geocoder/v2/?ak={0}&location={1},{2}&output=json&pois=0";

        /// <summary>
        /// 0.地址字符串 1.ak
        /// </summary>
        private const string BaiduGeoCoding_ApiCoord = "http://api.map.baidu.com/geocoder/v2/?ak={0}&address={1}&output=json";
        #endregion

        #region 地址转换器
        /// <summary>
        /// 根据地址信息 获取 经纬度
        /// </summary>
        /// <param name="addressStr">地址字符串</param>
        /// <returns></returns>
        public static BaiDuGeoCoding AddressToCoordinate(string addressStr)
        {
            string url = string.Format(Baidu_GeoCoding_ApiCoord, addressStr);
            var http = new HttpUtil();
            #region (x.1)headers
            var headers = new Dictionary<string, string>();
            headers["Accept"] = "application/json;charset=utf-8";
            headers["Content-Type"] = "application/json;charset=utf-8";
            #endregion
            var jsonstr = http.Ajax_Get(url, headers);
            var model = jsonstr.ToString().Deserialize<BaiDuGeoCoding>();
            return model;
        }

        /// <summary>
        /// 根据经纬度  获取 地址信息
        /// </summary>
        /// <param name="lat">经度</param>
        /// <param name="lng">纬度</param>
        /// <returns></returns>
        public static BaiDuGeoCoding CoordinateToAddress(object lat, string lng)
        {
            string url = string.Format(Baidu_GeoCoding_ApiUrl, lat, lng);
            var http = new HttpUtil();
            #region (x.1)headers
            var headers = new Dictionary<string, string>();
            headers["Accept"] = "application/json;charset=utf-8";
            headers["Content-Type"] = "application/json;charset=utf-8";
            #endregion
            var jsonstr = http.Ajax_Get(url, headers);
            var model = jsonstr.ToString().Deserialize<BaiDuGeoCoding>();
            return model;
        }
        #endregion

        #region 辅助格式化
        /// <summary>
        /// /// <summary>
        /// 经纬度  逆地理编码 Url  需要Format 0.经度  1.纬度 
        /// </summary>
        /// </summary>
        public static string Baidu_GeoCoding_ApiUrl
        {
            get
            {
                return string.Format(BaiduGeoCoding_ApiUrl, BaiduAk, "{0}", "{1}");
            }
        }

        /// <summary>
        /// 地址字符串 
        /// </summary>
        public static string Baidu_GeoCoding_ApiCoord
        {
            get
            {
                return string.Format(BaiduGeoCoding_ApiCoord, BaiduAk, "{0}");
            }
        }


        #endregion

        #region model
        public class BaiDuGeoCoding
        {
            public int Status { get; set; }
            public Result Result { get; set; }
        }

        public class Result
        {
            public Location Location { get; set; }

            public string Formatted_Address { get; set; }

            public string Business { get; set; }

            public AddressComponent AddressComponent { get; set; }

            public string CityCode { get; set; }
        }

        public class AddressComponent
        {
            /// <summary>
            /// 省份
            /// </summary>
            public string Province { get; set; }
            /// <summary>
            /// 城市名
            /// </summary>
            public string City { get; set; }

            /// <summary>
            /// 区县名
            /// </summary>
            public string District { get; set; }

            /// <summary>
            /// 街道名
            /// </summary>
            public string Street { get; set; }

            public string Street_number { get; set; }

        }

        public class Location
        {
            public string Lng { get; set; }
            public string Lat { get; set; }
        }
        #endregion
        //地球半径，单位米
        private const double EARTH_RADIUS = 6378137;
        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns></returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }
    }
}