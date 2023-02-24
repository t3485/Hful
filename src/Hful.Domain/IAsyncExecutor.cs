using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Domain
{
    public interface IAsyncExecutor
    {
        Task<List<T>> ToListAsync<T>(IQueryable<T> queryable);
    }
}
