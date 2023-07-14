using Hful.Core.Context;

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

        public Guid? Id { get => GuidParse(Find("id")); }

        public string? UserName { get => Find("username"); }

        public bool IsSuperAdmin { get => true; }

        public List<Guid> RoleIds { get; }

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
