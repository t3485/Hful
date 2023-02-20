using Microsoft.Extensions.DependencyInjection;

namespace Hful.Module
{
    public abstract class HfulModule
    {
        public virtual void ConfigureServices(HfulModuleContext context)
        {
        }
    }
}
