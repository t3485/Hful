using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hful.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddModule<T>(this IServiceCollection services, IConfiguration configuration)
        {
            new ModuleRunner<T>().ConfigureServices(services, configuration);
        }
    }
}
