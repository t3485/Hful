using Hful.Domain;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.EntityFrameworkCore.Repository
{
    internal class AsyncExecutor : IAsyncExecutor
    {
        public Task<List<T>> ToListAsync<T>(IQueryable<T> queryable)
        {
            return queryable.ToListAsync();
        }
    }
}
