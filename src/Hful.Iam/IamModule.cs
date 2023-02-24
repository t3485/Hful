using Hful.Iam.Permissions;
using Hful.Module;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Iam
{
    public class IamModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
            context.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            context.Services.AddSingleton<IAuthorizationPolicyProvider, HfulAuthorizationPolicyProvider>();
        }
    }
}
