using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hful.Iam.Permissions
{
    public class TestAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider
    {
        public TestAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options) { }

        public override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            //if (policyName.StartsWith(Permissions.User))
            //{
            //    var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
            //    policy.AddRequirements(new PermissionAuthorizationRequirement(policyName));
            //    return Task.FromResult<AuthorizationPolicy?>(policy.Build());
            //}
            return base.GetPolicyAsync(policyName);
        }
    }
}
