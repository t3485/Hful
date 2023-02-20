using Hful.Iam.Api.Dto.Authorization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

using System.Text;

namespace Hful.Iam.Api.Controllers
{
    [ApiController]
    [Route("iam")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthorizationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var jwtConfig = _configuration.GetSection("Jwt");
            //秘钥，就是标头，这里用Hmacsha256算法，需要256bit的密钥
            var securityKey = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.GetValue<string>("Secret"))), SecurityAlgorithms.HmacSha256);
            //Claim，JwtRegisteredClaimNames中预定义了好多种默认的参数名，也可以像下面的Guid一样自己定义键名.
            //ClaimTypes也预定义了好多类型如role、email、name。Role用于赋予权限，不同的角色可以访问不同的接口
            //相当于有效载荷
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Iss, jwtConfig.GetValue<string>("Issuer")),
                new Claim(JwtRegisteredClaimNames.Aud, jwtConfig.GetValue<string>("Audience")),
                new Claim("Guid", Guid.NewGuid().ToString("D")),
                new Claim(ClaimTypes.Role, "system"),
                new Claim(ClaimTypes.Role, "admin")
            };
            SecurityToken securityToken = new JwtSecurityToken(
                signingCredentials: securityKey,
                expires: DateTime.Now.AddMinutes(2),//过期时间
                claims: claims
            );
            return Content(new JwtSecurityTokenHandler().WriteToken(securityToken));
        }

        [Route("logout")]
        [HttpPost]
        public async Task LogoutAsync()
        {

        }
    }
}
