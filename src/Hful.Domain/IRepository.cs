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

        Task<T?> FindById(Guid id);

        Task SaveAsync(T entity);

        Task DeleteAsync(Guid id);

        Task DeleteAsync(List<Guid> id);

        Task SaveAsync(List<T> entities);

        Task<ITransaction> BeginTransactionAsync();
    }
}
