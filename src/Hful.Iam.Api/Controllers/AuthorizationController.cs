using Hful.Domain;
using Hful.Domain.Iam;
using Hful.Iam.Api.Dto.Authorization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IAsyncExecutor _asyncExecutor;

        public AuthorizationController(IConfiguration configuration,
            IRepository<User> userRepository,
            IAsyncExecutor asyncExecutor)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _asyncExecutor = asyncExecutor;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var entity = _asyncExecutor.FirstAsync(_userRepository.AsQueryable().Where(x => x.UserName == dto.Username));




            var jwtConfig = _configuration.GetSection("Jwt");
            var securityKey = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.GetValue<string>("SecretKey"))), SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Iss, jwtConfig.GetValue<string>("Issuer")),
                new Claim(JwtRegisteredClaimNames.Aud, jwtConfig.GetValue<string>("Audience")),
                new Claim("id", entity.Id.ToString("D")),
                new Claim(ClaimTypes.Role, "system"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim("Permission", "iam_user")
            };
            SecurityToken securityToken = new JwtSecurityToken(
                signingCredentials: securityKey,
                expires: DateTime.Now.AddHours(2),//过期时间
                claims: claims
            );
            return Content(new JwtSecurityTokenHandler().WriteToken(securityToken));
        }

        [Route("logout")]
        [HttpPost]
        [Authorize]
        public async Task LogoutAsync()
        {

        }
    }
}
