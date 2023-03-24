using Hful.Domain.Iam;
using Hful.Iam.Domain;

using Microsoft.EntityFrameworkCore;

namespace Hful.EntityFrameworkCore.Extensions
{
    public static class UserCreatingModuleExtension
    {
        public static void ConfigureUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("iam_user");
                b.HasIndex(x => x.UserName).IsUnique();
                b.Property(x => x.UserName).IsRequired().HasMaxLength(32);
                b.Property(x => x.Password).IsRequired().HasMaxLength(128);
                b.Property(x => x.DisplayName).IsRequired().HasMaxLength(32);
                b.Property(x => x.Email).HasMaxLength(128);
                b.Property(x => x.Phone).HasMaxLength(32);
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("iam_role");
                b.HasIndex(x => x.Code).IsUnique();
                b.Property(x => x.Code).IsRequired().HasMaxLength(32);
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("iam_user_role");
                b.HasIndex(x => new { x.UserId, x.RoleId }).IsUnique();
            });

            modelBuilder.Entity<Permission>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("iam_permission");
                b.HasIndex(x => x.Code).IsUnique();
                b.Property(x => x.Code).IsRequired().HasMaxLength(32);
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });

            modelBuilder.Entity<RolePermission>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("iam_role_permission");
                b.HasIndex(x => new { x.RoleId, x.PermissionId }).IsUnique();
            });

            modelBuilder.Entity<UserPermission>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("iam_user_permission");
            });

            modelBuilder.Entity<Menu>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("iam_menu");
                b.HasIndex(x => x.Code).IsUnique();
                b.Property(x => x.Code).IsRequired().HasMaxLength(32);
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("iam_tenant");
                b.HasIndex(x => x.Code).IsUnique();
                b.Property(x => x.Code).IsRequired().HasMaxLength(32);
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });
        }
    }
}
