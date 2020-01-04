﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace SAAS.FrameWork.Extensions
{
    public static partial class JTokenDeserializeExtensions
    {
        #region DeserializeByString

        
        /// <summary>
        /// 反序列化（token亦可为值类型,如 int、bool、string）
        /// (先转换为string,再反序列化)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="token"></param>
        /// <returns></returns>
        public static object DeserializeByString(this JToken token, Type type)
        {
            if (token.IsNull()) return type.DefaultValue();

            string strValue = token.GetValue().Convert<string>();
            return strValue.Deserialize(type);
        }

        /// <summary>
        /// 反序列化（token亦可为值类型,如 int、bool、string）
        /// (先转换为string,再反序列化)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="token"></param>
        /// <returns></returns>
        public static T DeserializeByString<T>(this JToken token)
        {
            return (T)DeserializeByString(token, typeof(T));
        }



        #endregion



        #region Deserialize(Type type)
        /// <summary>
        /// 反序列化（token亦可为值类型,如 int、bool、string）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="token"></param>
        /// <returns></returns>
        public static object Deserialize(this JToken token, Type type)
        {
            if (token.IsNull()) return type.DefaultValue();

            if (type.TypeIsValueTypeOrStringType())
            {
                return DeserializeStruct(token, type);
            }
            return DeserializeClass(token, type);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">必须为 where T : struct 或者String</typeparam>
        /// <param name="token"></param>
        /// <returns></returns>
        private static object DeserializeStruct(JToken token, Type type)
        {            
            try
            {
                if (type == typeof(string))
                {
                    return token.ConvertToString();
                }
                return token.GetValue().Convert(type);
            }
            catch { }
            return type.DefaultValue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type">必须为 where T : class</param>
        /// <returns></returns>
        private static object DeserializeClass(JToken token, Type type)
        {          
            string str = "" + token.GetValue();
            if (string.IsNullOrWhiteSpace(str)) return type.DefaultValue();
            return JsonConvert.DeserializeObject(str, type);
        }


        #endregion


        #region Deserialize<T>
        /// <summary>
        /// 反序列化（token亦可为值类型,如 int、bool、string）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="token"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this JToken token)
        {
            return (T)Deserialize(token,typeof(T));            
        }         
        #endregion

    }
}
