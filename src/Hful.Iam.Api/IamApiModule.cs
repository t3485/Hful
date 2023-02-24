using Hful.Module;

namespace Hful.Iam.Api
{
    [HfulDependOn(typeof(IamModule))]
    public class IamApiModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
        }
    }
}
