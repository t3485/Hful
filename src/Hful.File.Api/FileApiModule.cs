using Hful.Core;
using Hful.Core.Options;

namespace Hful.File.Api
{
    [HfulDependOn(typeof(FileModule))]
    public class FileApiModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
            Config<WebOptions>(x =>
            {
                x.AddControllerAssembly(typeof(FileApiModule).Assembly);
            });
        }
    }
}
