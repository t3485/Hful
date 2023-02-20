using Hful.EntityFrameworkCore;
using Hful.Iam.Api;
using Hful.Module;

namespace Hful.Web
{
    [HfulDependOn(typeof(EfModule),
        typeof(IamApiModule))]
    public class WebModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
        }
    }
}
