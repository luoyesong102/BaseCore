using System.Collections.Generic;
using System.Threading.Tasks;
using SAAS.Dapper.Extension.Model;

namespace SAAS.Dapper.Extension.Core.Interfaces
{
    public interface IQuery<T>
    {
        T Get();

        Task<T> GetAsync();

        IEnumerable<T> ToList();

        Task<IEnumerable<T>> ToListAsync();

        PageList<T> PageList(int pageIndex, int pageSize);
    }
}
