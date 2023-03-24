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
                        Name = "Test",
                        Code = "Test",
                    }
                };
                context.Menus.AddRange(menus);

                context.SaveChanges();
            }
        }
    }
}
