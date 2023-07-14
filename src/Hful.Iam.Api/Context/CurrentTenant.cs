using Hful.Core.Context;

using System.IdentityModel.Tokens.Jwt;

namespace Hful.Iam.Api.Context
{
    internal class CurrentTenant : ICurrentTenant
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentTenant(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? Id { get => GuidParse(Find("tenantId")); }

        private string? Find(string name)
        {
            string? auth = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
            string? jwt = auth?[(auth.IndexOf(' ') + 1)..];

            if (string.IsNullOrWhiteSpace(jwt))
            {
                return null;
            }

            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
            return token.Claims.FirstOrDefault(x => x.Type == name)?.Value;
        }

        private static Guid? GuidParse(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            return Guid.Parse(value);
        }
    }
}
