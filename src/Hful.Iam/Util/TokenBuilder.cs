using Hful.Iam.Dto;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hful.Iam.Util
{
    public class TokenBuilder
    {
        private string secretKey, issuer, audience;
        private Guid userId;
        private List<string> permissions;

        public TokenBuilder SetFromUserDto(UserDto dto)
        {
            userId = dto.Id;
            return this;
        }

        public TokenBuilder SetPermissions(List<string> permissions)
        {
            this.permissions = permissions;
            return this;
        }

        public TokenBuilder ReadFromConfiguration(IConfiguration configuration)
        {
            secretKey = configuration.GetValue<string>("SecretKey");
            issuer = configuration.GetValue<string>("Issuer");
            audience = configuration.GetValue<string>("Audience");
            return this;
        }

        public string Build()
        {
            var securityKey = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Iss, issuer),
                new Claim(JwtRegisteredClaimNames.Aud, audience),
                new Claim("id", userId.ToString("D")),
                new Claim(ClaimTypes.Role, "system"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim("permission", string.Join(',', permissions))
            };

            var identity = new ClaimsIdentity(new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme));
            identity.AddClaims(claims);

            SecurityToken securityToken = new JwtSecurityToken(
                signingCredentials: securityKey,
                expires: DateTime.Now.AddHours(2),//过期时间
                claims: claims
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
