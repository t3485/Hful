using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hful.Core
{
    public class HfulModuleContext
    {
        public IServiceCollection Services { get; internal set; }

        public IConfiguration Configuration { get; internal set; }
    }
}