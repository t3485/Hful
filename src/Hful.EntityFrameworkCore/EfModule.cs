using Hful.Core;
using Hful.Core.UnitOfWork;
using Hful.Domain;
using Hful.EntityFrameworkCore.Extensions;
using Hful.EntityFrameworkCore.Repository;
using Hful.EntityFrameworkCore.UnitOfWork;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hful.EntityFrameworkCore
{
    public class EfModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
            context.Services.AddDbContext<HfulContext>(options => options.UseMySql(context.Configuration.GetConnectionString("Default"), new MySqlServerVersion(new Version())));
            context.Services.AddDatabaseDeveloperPageExceptionFilter();

            context.AddAutoRepository();

            context.Services.AddSingleton<IAsyncExecutor, AsyncExecutor>();
            context.Services.AddScoped<IUowManager, UowManager>();
        }
    }
}
