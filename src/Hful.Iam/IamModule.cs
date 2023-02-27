using Hful.Core;
using Hful.Iam.Permissions;
using Hful.Iam.Service;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Hful.Iam
{
    public class IamModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
            context.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            context.Services.AddSingleton<IAuthorizationPolicyProvider, HfulAuthorizationPolicyProvider>();

            context.Services.AddTransient<IUserService, UserService>(); 
        }
    }
}
