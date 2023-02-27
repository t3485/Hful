using Hful.Domain.Iam;
using Hful.Iam.Api.Dto.Authorization;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<User> _signInManager;

        public AuthorizationController(IConfiguration configuration, SignInManager<User> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var jwtConfig = _configuration.GetSection("Jwt");
            //秘钥，就是标头，这里用Hmacsha256算法，需要256bit的密钥
            var securityKey = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.GetValue<string>("SecretKey"))), SecurityAlgorithms.HmacSha256);
            //Claim，JwtRegisteredClaimNames中预定义了好多种默认的参数名，也可以像下面的Guid一样自己定义键名.
            //ClaimTypes也预定义了好多类型如role、email、name。Role用于赋予权限，不同的角色可以访问不同的接口
            //相当于有效载荷
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Iss, jwtConfig.GetValue<string>("Issuer")),
                new Claim(JwtRegisteredClaimNames.Aud, jwtConfig.GetValue<string>("Audience")),
                new Claim("guid", Guid.NewGuid().ToString("D")),
                new Claim(ClaimTypes.Role, "system"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim("Permission", "iam_user")
            };

            var identity = new ClaimsIdentity(new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme));
            identity.AddClaims(claims);

            await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            SecurityToken securityToken = new JwtSecurityToken(
                signingCredentials: securityKey,
                expires: DateTime.Now.AddHours(2),//过期时间
                claims: claims
            );
            return Content(new JwtSecurityTokenHandler().WriteToken(securityToken));
        }

        [Route("logout")]
        [HttpPost]
        public Task LogoutAsync()
        {
            return HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
        }
    }
}
