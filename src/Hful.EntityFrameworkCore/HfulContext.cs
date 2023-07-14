using Hful.Core.Context;
using Hful.Domain.Shared;
using Hful.Domain.Shared.ModelCreation;
using Hful.EntityFrameworkCore.ModelCreation;

using Microsoft.EntityFrameworkCore;

namespace Hful.EntityFrameworkCore
{
    public class HfulContext : DbContext
    {
        private readonly ICurrentUser _currentUser;

        public HfulContext(DbContextOptions<HfulContext> options, ICurrentUser currentUser)
            : base(options)
        {
            _currentUser = currentUser;
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IModelBuilder builder = new HfulModelBuilder(modelBuilder);
            foreach (var item in ModelExtension.Actions)
            {
                item.Invoke(builder);
            }
        }

        public override int SaveChanges()
        {
            SetAutoField();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAutoField();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAutoField()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is AuditedEntity entity)
                {
                    if (item.State == EntityState.Added)
                    {
                        entity.CreatedTime = DateTime.Now;
                        entity.CreatedBy = _currentUser.Id ?? Guid.Empty;
                    }
                    else if (item.State == EntityState.Modified)
                    {
                        entity.UpdatedTime = DateTime.Now;
                        entity.UpdatedBy = _currentUser.Id;
                    }
                }
            }
        }
    }
}
