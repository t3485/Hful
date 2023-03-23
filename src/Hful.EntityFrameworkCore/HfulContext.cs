using Hful.Domain.Iam;
using Hful.Domain.Shared;
using Hful.EntityFrameworkCore.Extensions;
using Hful.Iam.Domain;

using Microsoft.EntityFrameworkCore;

namespace Hful.EntityFrameworkCore
{
    public class HfulContext : DbContext
    {
        public HfulContext(DbContextOptions<HfulContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUser();
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
                    }
                    else if (item.State == EntityState.Modified)
                    {
                        entity.UpdatedTime = DateTime.Now;
                    }
                }
            }
        }
    }
}
