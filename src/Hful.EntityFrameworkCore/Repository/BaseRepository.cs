using EFCore.BulkExtensions;

using Hful.Domain;
using Hful.Domain.Shared;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace Hful.EntityFrameworkCore.Repository
{
    internal class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly HfulContext context;

        public BaseRepository(HfulContext context)
        {
            this.context = context;
        }

        public IQueryable<T> AsQueryable()
        {
            return context.Set<T>().AsQueryable().AsNoTracking();
        }

        public async Task SaveAsync(T entity)
        {
            if (entity.Id == Guid.Empty)
            {
                CheckAndSetId(entity);
                await context.Set<T>().AddAsync(entity);
            }
            else
            {
                context.Set<T>().Update(entity);
            }
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(IEnumerable<Guid> id)
        {
            var entities = await context.Set<T>().Where(x => id.Contains(x.Id)).ToListAsync();
            foreach (var item in entities)
            {
                context.Set<T>().Remove(item);
            }
            await context.SaveChangesAsync();
        }

        public async Task SaveAsync(List<T> entities)
        {
            foreach (var item in entities)
            {
                CheckAndSetId(item);
            }

            await context.BulkInsertOrUpdateAsync(entities);
            await context.BulkSaveChangesAsync();
        }

        public async Task SaveAsync<V>(List<T> entities, Expression<Func<T, V>> f)
        {
            foreach (var item in entities)
            {
                CheckAndSetId(item);
            }

            //var model = context.Model.FindEntityType(typeof(T));
            //var index = model.GetIndexes().Where(x => x.IsUnique).ToList();
            //var indexProperty = index.SelectMany(x => x.Properties).Select(x => typeof(T).GetProperty(x.Name)).ToList();

            //await context.BulkInsertOrUpdateAsync(entities, new BulkConfig()
            //{
            //    UpdateByProperties = indexProperty.Select(x => x.Name).ToList()
            //});

            List<string> update = new List<string>();

            if (f.Body is NewExpression newExp)
            {
                update = newExp.Arguments.Select(x => (x as MemberExpression).Member.Name).ToList();
            }
            else if (f.Body is MemberExpression memExp)
            {
                update = new List<string> { memExp.Member.Name };
            }

            await context.BulkSaveChangesAsync();
        }

        public async Task<T?> FindByIdAsync(Guid id)
        {
            return await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<T>> FindByIdAsync(IEnumerable<Guid> id)
        {
            return await context.Set<T>().Where(x => id.Contains(x.Id)).ToListAsync();
        }

        public async Task<ITransaction> BeginTransactionAsync()
        {
            var transaction = await context.Database.BeginTransactionAsync();
            return new EfCoreTransaction(transaction);
        }

        protected virtual void CheckAndSetId(T entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
        }
    }
}
