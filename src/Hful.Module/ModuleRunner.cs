using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Module
{
    internal class ModuleRunner
    {
        private List<HfulModule> _instance;

        public void ConfigureServices<T>(IServiceCollection services, IConfiguration configuration)
        {
            var context = new HfulModuleContext()
            {
                Services = services,
                Configuration = configuration
            };
            foreach (var item in GetModuleInstance(typeof(T)))
            {
                item.ConfigureServices(context);
            }
        }

        private IEnumerable<HfulModule> GetModuleInstance(Type type)
        {
            return _instance ??= GetModules(type).Select(x => (HfulModule)Activator.CreateInstance(x)).ToList();
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
