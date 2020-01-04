using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityS4.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityS4.Services
{
    public class BaseService<T> where T : class, new()
    {
        public EFContext db;

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Add(T entity)
        {
            await db.Set<T>().AddAsync(entity);
            return db.SaveChanges() > 0;
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Del(T entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
            int i = await db.SaveChangesAsync();
            return i > 0;
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Edit(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            int i = await db.SaveChangesAsync();
            return i > 0;
        }
        /// <summary>
        /// 查-根据传进来的lambda表达式查询一条数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            T t = await db.Set<T>().Where(whereLambda).SingleOrDefaultAsync();
            return t;
        }

        /// <summary>
        /// 查询多条数据-根据传进来的lambda和排序的lambda查询
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public async Task<List<T>> GetList<s>(int pageIndex, int pageSize,
            System.Linq.Expressions.Expression<Func<T, bool>> whereLambda,
            System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            var temp = db.Set<T>().Where(whereLambda);
            List<T> list = null;
            if (isAsc)//升序
            {
                list = await temp.OrderBy(orderbyLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else//降序
            {
                list = await temp.OrderByDescending(orderbyLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            return list;
        }

        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<int> GetTotalCount(Expression<Func<T, bool>> whereLambda)
        {
            return await db.Set<T>().Where(whereLambda).CountAsync();
        }
    }
}
