using Hful.Core;
using Hful.Core.Options;
using Hful.Iam.Api.Context;
using Hful.Iam.Service;

namespace Hful.Iam.Api
{
    [HfulDependOn(typeof(IamModule))]
    public class IamApiModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
            Config<WebOptions>(x =>
            {
                x.AddControllerAssembly(typeof(IamApiModule).Assembly);
            });

            context.Services.AddTransient<ICurrentUser, CurrentUser>();
            context.Services.AddTransient<ICurrentTenant, CurrentTenant>();

            context.Services.AddCaptcha(context.Configuration);
        }
    }
}
