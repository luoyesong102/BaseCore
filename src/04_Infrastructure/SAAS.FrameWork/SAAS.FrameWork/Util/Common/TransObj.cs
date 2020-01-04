using SAAS.FrameWork.Module.SsApiDiscovery.ApiDesc.Attribute;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SAAS.FrameWork.Util.Common
{
        public static class TransObj<TIn, TOut>
        {

            private static readonly Func<TIn, TOut> cache = GetFunc();
            private static Func<TIn, TOut> GetFunc()
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
                List<MemberBinding> memberBindingList = new List<MemberBinding>();

                foreach (var item in typeof(TOut).GetProperties())
                {
                    if (!item.CanWrite)
                        continue;

                    MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }

                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
                Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[] { parameterExpression });

                return lambda.Compile();
            }

            public static TOut Trans(TIn tIn)
            {
                return cache(tIn);
            }

        }

    public static class  TransObj
    {
        public static DataSet ToDataSet<TSource>(this IList<TSource> list)
        {
            Type elementType = typeof(TSource);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);

            foreach (var pi in elementType.GetProperties())
            {
                Type colType = Nullable.GetUnderlyingType(pi.PropertyType) ?? pi.PropertyType;
             
              var descobj=  (SsDescriptionAttribute)Attribute.GetCustomAttribute(pi, typeof(SsDescriptionAttribute));
                if (descobj == null)
                {
                    dt.Columns.Add(pi.Name, colType);
                }
                else
                {
                    dt.Columns.Add(descobj.Value, colType);
                }
            }

            foreach (TSource item in list)
            {
                DataRow row = dt.NewRow();
                foreach (var pi in elementType.GetProperties())
                {
                    var descobj = (SsDescriptionAttribute)Attribute.GetCustomAttribute(pi, typeof(SsDescriptionAttribute));
                    var name = "";
                    if(descobj==null)
                    {
                        name = pi.Name;
                       
                    }
                    else
                    {
                        name = descobj.Value;
                    }
                    row[name] = pi.GetValue(item, null) ?? DBNull.Value;
                }
                dt.Rows.Add(row);
            }
         
            return ds;
        }
    }
  
}