using Hful.Domain.Iam;
using Hful.Iam.Domain;

namespace Hful.EntityFrameworkCore
{
    public class DbInitializer
    {
        public static void Initialize(HfulContext context)
        {
            if (!context.Users.Any())
            {
                var user = new User
                {
                    UserName = "admin",
                    Password = "123456",
                    DisplayName = "admin",
                };
                context.Users.Add(user);

                var role = new Role
                {
                    Code = "admin",
                    Name = "admin"
                };
                context.Roles.Add(role);

                var tenant = new Tenant
                {
                    Code = "Test",
                    Name = "Test"
                };
                context.Tenants.Add(tenant);

                var menus = new List<Menu>()
                {
                    new Menu
                    {
                        Name = "menu1",
                        Code = "menu1",
                    },
                    new Menu
                    {
                        Name = "menu2",
                        Code = "menu2",
                    },
                    new Menu
                    {
                        Name = "menu3",
                        Code = "menu3",
                    }
                };
                context.Menus.AddRange(menus);

                menus = new List<Menu>()
                {
                    new Menu
                    {
                        Name = "menu1-1",
                        Code = "menu1-1",
                        ParentId = menus[0].Id
                    },
                    new Menu
                    {
                        Name = "menu1-2",
                        Code = "menu1-2",
                        ParentId = menus[0].Id
                    },
                    new Menu
                    {
                        Name = "menu1-3",
                        Code = "menu1-3",
                        ParentId = menus[0].Id
                    }
                };
                context.Menus.AddRange(menus);

                context.SaveChanges();
            }
        }
    }
}
