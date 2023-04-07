using Hful.Core;
using Hful.File.Service;

using Microsoft.Extensions.DependencyInjection;

namespace Hful.File
{
    [HfulDependOn(typeof(CoreModule))]
    public class FileModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
            context.Services.AddTransient<IAttachmentService, AttachmentService>();
        }
    }
}
