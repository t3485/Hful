using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Hful.Core
{
    internal class ModuleRunner<T>
    {
        private readonly List<HfulModule> _instance;

        public ModuleRunner()
        {
            _instance = GetModules(typeof(T)).Select(x => (HfulModule?)Activator.CreateInstance(x)).Where(x => x != null).ToList();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var context = new HfulModuleContext()
            {
                Services = services,
                Configuration = configuration
            };
            foreach (var item in _instance)
            {
                item.ConfigureServices(context);
            }
        }

        private IEnumerable<Type> GetModules(Type type)
        {
            List<Type> result = new() { type };
            Dictionary<Type, int> dict = new() { [type] = 0 };

            for (int i = 0; i < result.Count; i++)
            {
                var depend = result[i].GetCustomAttribute<HfulDependOnAttribute>();
                if (depend != null)
                {
                    var types = depend.GetDependedTypes();
                    var exists = types.Where(dict.ContainsKey).ToList();
                    var min = exists.Any() ? exists.Select(x => dict.GetValueOrDefault(x)).Min() : i;

                    MoveTo(result, i, min);

                    foreach (var item in types.Where(x => !dict.ContainsKey(x)))
                    {
                        dict.Add(item, result.Count);
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        private static void MoveTo<T>(List<T> list, int fromIndex, int toIndex)
        {
            if (fromIndex == toIndex)
            {
                return;
            }

            T value = list[fromIndex];
            for (int i = fromIndex; i > toIndex; i--)
                list[i] = list[i - 1];
            list[toIndex] = value;
        }
    }
}
