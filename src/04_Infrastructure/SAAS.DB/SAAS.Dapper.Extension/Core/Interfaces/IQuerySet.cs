using System;
using System.Linq.Expressions;
using SAAS.Dapper.Extension.Core.SetQ;

namespace SAAS.Dapper.Extension.Core.Interfaces
{
    public interface IQuerySet<T>
    {
        QuerySet<T> Where(Expression<Func<T, bool>> predicate);

        QuerySet<T> WithNoLock();
    }
}
