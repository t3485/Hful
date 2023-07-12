using Hful.Core;
using Hful.Iam.Permissions;
using Hful.Iam.Service;
using Hful.Domain.Shared.ModelCreation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Hful.Domain.Iam;
using Hful.Iam.Domain;

namespace Hful.Iam
{
    [HfulDependOn(typeof(CoreModule))]
    public class IamModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
            context.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            context.Services.AddSingleton<IAuthorizationPolicyProvider, HfulAuthorizationPolicyProvider>();

            context.Services.AddTransient<IUserService, UserService>();
            context.Services.AddTransient<IRoleService, RoleService>();
            context.Services.AddTransient<ILoginService, LoginService>();
            context.Services.AddTransient<IPermissionService, PermissionService>();


            context.Services.AddDomain(modelBuilder =>
            {
                modelBuilder.Entity<User>(b =>
                {
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
                    b.ToTable("iam_role");
                    b.HasIndex(x => x.Code).IsUnique();
                    b.Property(x => x.Code).IsRequired().HasMaxLength(32);
                    b.Property(x => x.Name).IsRequired().HasMaxLength(128);
                });

                modelBuilder.Entity<UserRole>(b =>
                {
                    b.ToTable("iam_user_role");
                    b.HasIndex(x => new { x.UserId, x.RoleId }).IsUnique();
                });

                modelBuilder.Entity<Permission>(b =>
                {
                    b.ToTable("iam_permission");
                    b.HasIndex(x => x.Code).IsUnique();
                    b.Property(x => x.Code).IsRequired().HasMaxLength(32);
                    b.Property(x => x.Name).IsRequired().HasMaxLength(128);
                });

                modelBuilder.Entity<RolePermission>(b =>
                {
                    b.ToTable("iam_role_permission");
                    b.HasIndex(x => new { x.RoleId, x.PermissionId }).IsUnique();
                });

                modelBuilder.Entity<UserPermission>(b =>
                {
                    b.ToTable("iam_user_permission");
                    b.HasIndex(x => new { x.UserId, x.PermissionId }).IsUnique();
                });

                modelBuilder.Entity<Menu>(b =>
                {
                    b.ToTable("iam_menu");
                    b.HasIndex(x => x.Code).IsUnique();
                    b.Property(x => x.Code).IsRequired().HasMaxLength(32);
                    b.Property(x => x.Name).IsRequired().HasMaxLength(128);
                });

                modelBuilder.Entity<Tenant>(b =>
                {
                    b.ToTable("iam_tenant");
                    b.HasIndex(x => x.Code).IsUnique();
                    b.Property(x => x.Code).IsRequired().HasMaxLength(32);
                    b.Property(x => x.Name).IsRequired().HasMaxLength(128);
                });
            });
        }
    }
}
