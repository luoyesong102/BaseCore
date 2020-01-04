using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IdentityS4.Services
{
    public interface IBaseService<T> where T : class, new()
    {
        //增
        Task<bool> Add(T entity);


        //删
        Task<bool> Del(T entity);

        //改
        Task<bool> Edit(T entity);

        //查
        Task<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);


        //查询分页
        Task<List<T>> GetList<s>(int pageIndex, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc);

        //获取总条数
        Task<int> GetTotalCount(Expression<Func<T, bool>> whereLambda);
    }
}
