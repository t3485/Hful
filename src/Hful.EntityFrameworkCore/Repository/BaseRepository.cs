using EFCore.BulkExtensions;

using Hful.Domain;
using Hful.Domain.Shared;

using Microsoft.EntityFrameworkCore;

using NetTopologySuite.Index.HPRtree;

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
            return context.Set<T>().AsQueryable();
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

        public async Task SaveAsync(List<T> entities)
        {
            foreach (var item in entities)
            {
                CheckAndSetId(item);
            }

            var model = context.Model.FindEntityType(typeof(T));
            var index = model.GetIndexes().Where(x => x.IsUnique).ToList();
            var indexProperty = index.SelectMany(x => x.Properties).Select(x => typeof(T).GetProperty(x.Name)).ToList();

            await context.BulkInsertOrUpdateAsync(entities, new BulkConfig()
            {
                UpdateByProperties = indexProperty.Select(x => x.Name).ToList()
            });
            await context.BulkSaveChangesAsync();
        }

        public async Task<T?> FindById(Guid id)
        {
            return await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
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
