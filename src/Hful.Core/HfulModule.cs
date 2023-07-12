using Hful.Core.Module;

using Microsoft.Extensions.DependencyInjection;

namespace Hful.Core
{
    public abstract class HfulModule
    {
        internal IServiceCollection Services { get; set; }

        public virtual void ConfigureServices(HfulModuleContext context)
        {
        }

        public virtual void PostConfigureServices(HfulModuleContext context)
        {
        }

        public virtual void InitApplication(HfulModuleAppContext context)
        {

        }

        public void Config<T>(Action<T> opt) where T : class
        {
            Services.Configure(opt);
        }
    }
}
