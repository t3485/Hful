using Hful.Core.Context;
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

            string name = typeof(CoreModule).Assembly.FullName;
            name = name.Substring(0, name.IndexOf('.'));

            context.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith(name)));

            context.Services.AddSingleton<ICurrentUser, DefaultCurrentUser>();
        }
    }
}
