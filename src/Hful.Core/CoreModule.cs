using Hful.Core.Mapper;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Hful.Core
{
    public class CoreModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
            context.Services.AddSingleton<IObjectMapper, ObjectMapper>();

            context.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
