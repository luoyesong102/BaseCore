﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SAAS.FrameWork.Extensions;
using SAAS.FrameWork.Log;

namespace SAAS.FrameWork.Util.Extensible
{
    /// <summary>
    /// 可动态扩展属性，但是序列化时不处理动态扩展属性
    /// </summary>
    public class Extensible
    {
        [JsonIgnore]
        protected IDictionary<string, object> _extensionData;

        [JsonIgnore]
        public IDictionary<string, object> extensionData => _extensionData??(_extensionData=new Dictionary<string, object>());


        /// <summary>
        /// Extensible
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetData<T>(string key,T value)
        {
            extensionData[key] = value;
        }

        /// <summary>
        /// Extensible。若类型不匹配，则返回默认值（default(T)）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetData<T>(string key)
        {
            if (extensionData.TryGetValue(key,out var value))
            {
                if (typeof(T).IsAssignableFrom(value?.GetType()))
                    return (T)value;
            }
            return default(T);
        }

        /// <summary>
        /// Extensible。若类型不匹配，则通过Convert转换。若转换不通过，则返回默认值（default(T)）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetDataByConvert<T>(string key)
        {
            if (extensionData.TryGetValue(key, out var value))
            {
                if (typeof(T).IsAssignableFrom(value?.GetType()))
                    return (T)value;

                try
                {
                    return value.Convert<T>();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }                
            }
            return default(T);
        }

        /// <summary>
        /// Extensible。若类型不匹配，则通过Serialize转换。若转换不通过，则返回默认值（default(T)）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetDataBySerialize<T>(string key)
        {
            if (extensionData.TryGetValue(key, out var value))
            {
                if (typeof(T).IsAssignableFrom(value?.GetType()))
                    return (T)value;

                try
                {
                    return value.ConvertBySerialize<T>();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
            return default(T);
        }

    }
}
