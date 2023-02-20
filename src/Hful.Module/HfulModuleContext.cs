using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hful.Module
{
    public class HfulModuleContext
    {
        public IServiceCollection Services { get; set; }

        public IConfiguration Configuration { get; set; }
    }
}