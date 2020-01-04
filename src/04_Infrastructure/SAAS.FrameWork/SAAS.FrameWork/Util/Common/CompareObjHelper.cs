using SAAS.FrameWork.Module.SsApiDiscovery.ApiDesc.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SAAS.FrameWork.Util.Common
{

        /// <summary>
        /// 
        /// </summary>
        public class CompareResult
        {
            /// <summary>
            /// 
            /// </summary>
            public bool IsChange { get; set; }
            /// <summary>
            /// 变跟内容
            /// </summary>
            public string ChangeContent { get; set; }
        }

        /// <summary>
        /// 对比实体属性变更(利用反射)
        /// </summary>
        public static class CompareEntry
        {
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static CompareResult CompareDTO(object BeforeDTO, object AfterDTO)
            {
                CompareResult result = new CompareResult();
                bool b = false;

                if (BeforeDTO == null && AfterDTO != null)
                {
                    b = true;
                }
                else if (BeforeDTO != null && AfterDTO == null)
                {
                    b = true;
                }
                else if (BeforeDTO.Equals(DBNull.Value) && !AfterDTO.Equals(DBNull.Value))
                {
                    b = true;
                }
                else if (!BeforeDTO.Equals(DBNull.Value) && AfterDTO.Equals(DBNull.Value))
                {
                    b = true;
                }
                //else if (BeforeDTO.GetType() != AfterDTO.GetType())
                //{
                //    result.IsChange = true;
                //    return result;
                //}
                else if (BeforeDTO is int || BeforeDTO is short || BeforeDTO is long || BeforeDTO is float || BeforeDTO is double || BeforeDTO is decimal)
                {
                    if (BeforeDTO is int)
                    {
                        if (Convert.ToInt32(BeforeDTO) != Convert.ToInt32(AfterDTO))
                        {
                            b = true;
                        }
                    }
                    else if (BeforeDTO is short)
                    {
                        if (Convert.ToInt16(BeforeDTO) != Convert.ToInt16(AfterDTO))
                        {
                            b = true;
                        }
                    }
                    else if (BeforeDTO is long)
                    {
                        if (Convert.ToInt64(BeforeDTO) != Convert.ToInt64(AfterDTO))
                        {
                            b = true;
                        }
                    }
                    else if (BeforeDTO is float)
                    {
                        if (Convert.ToSingle(BeforeDTO) != Convert.ToSingle(AfterDTO))
                        {
                            b = true;
                        }
                    }
                    else if (BeforeDTO is double)
                    {
                        if (Convert.ToDouble(BeforeDTO) != Convert.ToDouble(AfterDTO))
                        {
                            b = true;
                        }
                    }
                    else if (BeforeDTO is decimal)
                    {
                        if (Convert.ToDecimal(BeforeDTO) == Convert.ToDecimal(AfterDTO))
                        {
                            b = true;
                        }
                    }
                }
                else
                {
                    StringBuilder content = new StringBuilder();
                    var beforeMembers = BeforeDTO.GetType().GetProperties();
                    var afterMembers = AfterDTO.GetType().GetProperties();
                    for (int i = 0; i < beforeMembers.Length; i++)
                    {
                        var beforeVal = beforeMembers[i].GetValue(BeforeDTO, null);
                        var afterVal = afterMembers[i].GetValue(AfterDTO, null);
                        var beforeValue = beforeVal == null ? null : beforeVal.ToString();
                        var afterValue = afterVal == null ? null : afterVal.ToString();
                        if (beforeValue != afterValue)
                        {
                            b = true;
                        string des = "";
                        try
                        {
                            des = ((SsDescriptionAttribute)Attribute.GetCustomAttribute(afterMembers[i], typeof(SsDescriptionAttribute))).Value;// 属性值beforeMembers[i].Name
                            content.Append(des + ":(" + beforeValue + "->" + afterValue + ")");
                        }
                        catch
                        {

                        }
                        }
                    }
                    result.IsChange = b;
                    result.ChangeContent = content.ToString();
                }

                return result;
            }

        /// <summary>
        /// 判断两个相同引用类型的对象的属性值是否相等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj1">对象1</param>
        /// <param name="obj2">对象2</param>
        /// <param name="type">按type类型中的属性进行比较</param>
        /// <returns></returns>
        public static bool CompareProperties<T>(T obj1, T obj2, Type type)
        {
            //为空判断
            if (obj1 == null && obj2 == null)
                return true;
            else if (obj1 == null || obj2 == null)
                return false;

            Type t = type;
            PropertyInfo[] props = t.GetProperties();
            foreach (var po in props)
            {
                if (IsCanCompare(po.PropertyType))
                {
                    if (!po.GetValue(obj1).Equals(po.GetValue(obj2)))
                    {
                        return false;
                    }
                }
                else
                {
                    return CompareProperties(po.GetValue(obj1), po.GetValue(obj2), po.PropertyType);
                }
            }

            return true;
        }

        /// <summary>
        /// 该类型是否可直接进行值的比较
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static bool IsCanCompare(Type t)
        {
            if (t.IsValueType)
            {
                return true;
            }
            else
            {
                //String是特殊的引用类型，它可以直接进行值的比较
                if (t.FullName == typeof(String).FullName)
                {
                    return true;
                }
                return false;
            }
        }


        }
}