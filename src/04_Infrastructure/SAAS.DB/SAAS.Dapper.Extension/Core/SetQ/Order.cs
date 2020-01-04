using System;
using System.Data;
using System.Linq.Expressions;
using SAAS.Dapper.Extension.Core.Interfaces;
using SAAS.Dapper.Extension.Model;

namespace SAAS.Dapper.Extension.Core.SetQ
{
    /// <summary>
    /// 排序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Order<T> : Option<T>, IOrder<T>
    {
        protected Order(IDbConnection conn, SqlProvider sqlProvider) : base(conn, sqlProvider)
        {

        }

        protected Order(IDbConnection conn, SqlProvider sqlProvider, IDbTransaction dbTransaction) : base(conn, sqlProvider, dbTransaction)
        {

        }

        /// <inheritdoc />
        public virtual Order<T> OrderBy<TProperty>(Expression<Func<T, TProperty>> field)
        {
            if (field != null)
                SqlProvider.SetContext.OrderbyExpressionList.Add(EOrderBy.Asc, field);

            return this;
        }

        /// <inheritdoc />
        public virtual Order<T> OrderByDescing<TProperty>(Expression<Func<T, TProperty>> field)
        {
            if (field != null)
                SqlProvider.SetContext.OrderbyExpressionList.Add(EOrderBy.Desc, field);

            return this;
        }
    }
}
