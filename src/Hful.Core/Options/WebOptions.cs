using System.Reflection;

namespace Hful.Core.Options
{
    public class WebOptions
    {
        public List<Assembly> ControllerAssemblies { get; set; } = new List<Assembly>();

        public WebOptions AddControllerAssembly(Assembly assembly)
        {
            ControllerAssemblies.Add(assembly);
            return this;
        }
    }
}
