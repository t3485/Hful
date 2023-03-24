using Hful.Core;
using Hful.Iam.Tenant.Service;
using Hful.Tenant.Service;

using Microsoft.Extensions.DependencyInjection;

namespace Hful.Iam.Tenant
{
    [HfulDependOn(typeof(CoreModule))]
    public class TenantModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
            context.Services.AddSingleton<ITenantService, TenantService>();
        }
    }
}
