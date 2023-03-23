using Hful.Domain.Iam;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.EntityFrameworkCore
{
    public class DbInitializer
    {
        public static void Initialize(HfulContext context)
        {
            if (!context.Users.Any())
            {
                var user = new User[]
                {
                    new User
                    {
                        UserName = "admin",
                        Password = "123456",
                        DisplayName = "admin",
                    }
                };
                context.Users.AddRange(user);
                context.SaveChanges();
            }
        }
    }
}
