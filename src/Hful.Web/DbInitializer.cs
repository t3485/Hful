using Hful.Domain.Iam;
using Hful.EntityFrameworkCore;
using Hful.Iam.Domain;

namespace Hful.Web
{
    public class DbInitializer
    {
        public static void Initialize(HfulContext context)
        {
            if (!context.Set<User>().Any())
            {
                var user = new User
                {
                    UserName = "admin",
                    Password = "123456",
                    DisplayName = "admin",
                };
                context.Set<User>().Add(user);

                var role = new Role
                {
                    Code = "admin",
                    Name = "admin"
                };
                context.Set<Role>().Add(role);

                var tenant = new Tenant
                {
                    Code = "Test",
                    Name = "Test"
                };
                context.Set<Tenant>().Add(tenant);

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
                context.Set<Menu>().AddRange(menus);

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
                context.Set<Menu>().AddRange(menus);

                context.SaveChanges();
            }
        }
    }
}
