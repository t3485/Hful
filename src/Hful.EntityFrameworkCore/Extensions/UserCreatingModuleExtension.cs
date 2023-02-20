using Hful.Domain.Iam;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                b.Property(x => x.Code).IsRequired().HasMaxLength(32);
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("iam_user_role");
            });

            modelBuilder.Entity<Permission>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("iam_permission");
                b.Property(x => x.Code).IsRequired().HasMaxLength(32);
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });

            modelBuilder.Entity<RolePermission>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("iam_role_permission");
            });

            modelBuilder.Entity<UserPermission>(b =>
            {
                b.ConfigAutoProperty();
                b.ToTable("iam_user_permission");
            });
        }
    }
}
