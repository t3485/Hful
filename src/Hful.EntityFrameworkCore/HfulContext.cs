using Hful.Domain.Iam;
using Hful.EntityFrameworkCore.Extensions;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUser();
        }
    }
}
