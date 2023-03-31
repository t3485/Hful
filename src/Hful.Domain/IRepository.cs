using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Domain
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();

        Task<T?> FindByIdAsync(Guid id);

        Task<List<T>> FindByIdAsync(IEnumerable<Guid> id);

        Task SaveAsync(T entity);

        Task DeleteAsync(Guid id);

        Task DeleteAsync(IEnumerable<Guid> id);

        Task SaveAsync(List<T> entities);

        Task<ITransaction> BeginTransactionAsync();
    }
}
