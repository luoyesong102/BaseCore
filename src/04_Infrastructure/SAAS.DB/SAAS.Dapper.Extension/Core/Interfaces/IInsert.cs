﻿using System.Threading.Tasks;

namespace SAAS.Dapper.Extension.Core.Interfaces
{
    public interface IInsert<T>
    {
        int Insert(T entity);

        Task<int> InsertAsync(T entity);
    }
}
