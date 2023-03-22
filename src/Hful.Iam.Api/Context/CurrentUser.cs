using Hful.Iam.Service;

using System.IdentityModel.Tokens.Jwt;

namespace Hful.Iam.Api.Context
{
    internal class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid Id { get => Guid.Parse(Find("id")); }

        public string UserName { get => Find("username"); }

        public Guid TenantId { get => Guid.Parse(Find("tenantId")); }

        private string Find(string name)
        {
            string auth = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            string jwt = auth.Substring(auth.IndexOf(' ') + 1);

            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
            return token.Claims.FirstOrDefault(x => x.Type == name)?.Value;
        }
    }
}
