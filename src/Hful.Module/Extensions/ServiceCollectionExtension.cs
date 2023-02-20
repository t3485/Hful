using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Module.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddModule<T>(this IServiceCollection services, IConfiguration configuration)
        {
            new ModuleRunner().ConfigureServices<T>(services, configuration);
        }
    }
}
