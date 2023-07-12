using Hful.Core;
using Hful.Domain;
using Hful.Domain.Shared;
using Hful.EntityFrameworkCore.Repository;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Hful.EntityFrameworkCore.Extensions
{
    internal static class RepositoryExtension
    {
        public static void AddAutoRepository(this HfulModuleContext context)
        {
            // todo 修改
            string fullName = Assembly.GetExecutingAssembly().FullName;
            string firstName = fullName[..(fullName.IndexOf('.') - 1)];

            var baseType = typeof(BaseEntity);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetExportedTypes())
                .Where(baseType.IsAssignableFrom);

            var repositoryType = typeof(IRepository<>);
            foreach ( var type in types)
            {
                context.Services.AddTransient(repositoryType.MakeGenericType(type),
                    typeof(BaseRepository<>).MakeGenericType(type));
            }
        }
    }
}
