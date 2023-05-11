using Microsoft.Extensions.DependencyInjection;

namespace Hful.Core
{
    public abstract class HfulModule
    {
        internal IServiceCollection Services { get; set; }

        public virtual void ConfigureServices(HfulModuleContext context)
        {
        }

        public void Config<T>(Action<T> opt) where T : class
        {
            Services.Configure(opt);
        }
    }
}
