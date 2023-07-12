using Microsoft.Extensions.DependencyInjection;

namespace Hful.Domain.Shared.ModelCreation
{
    public static class ModelExtension
    {
        public static List<Action<IModelBuilder>> Actions { get; } = new List<Action<IModelBuilder>>();

        public static void AddDomain(this IServiceCollection services, Action<IModelBuilder> action)
        {
            Actions.Add(action);
        }
    }
}
