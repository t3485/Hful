using Hful.Core;
using Hful.Core.Options;

using Microsoft.Extensions.Options;

namespace Hful.Extensions.Web
{
    public static class ControllerExtension
    {
        public static void AddControllers(this HfulModuleContext context)
        {
            using var provider = context.Services.BuildServiceProvider();

            var webOptions = provider.GetService<IOptions<WebOptions>>();
            var mvcBuilder = context.Services.AddControllers();

            if (webOptions != null && webOptions.Value != null)
            {
                foreach (var item in webOptions.Value.ControllerAssemblies)
                {
                    mvcBuilder = mvcBuilder.AddApplicationPart(item);
                }
            }
        }
    }
}
