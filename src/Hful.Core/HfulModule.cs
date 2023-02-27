using Hful.Core.Mapper;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Hful.Core
{
    public abstract class HfulModule
    {
        public virtual void ConfigureServices(HfulModuleContext context)
        {
        }
    }
}
