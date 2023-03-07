using Hful.Domain;
using Hful.Domain.Iam;
using Hful.Iam.Api.Dto.Authorization;
using Hful.Iam.Service;
using Hful.Iam.Util;

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
        private readonly ILoginService _loginService;

        public AuthorizationController(IConfiguration configuration,
            ILoginService loginService)
        {
            _configuration = configuration;
            _loginService = loginService;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var user = await _loginService.LoginAsync(dto.Username, dto.Password);
            if (!user.Status || user.User == null)
            {
                return Unauthorized();
            }

            var jwtConfig = _configuration.GetSection("Jwt");
            return Content(new TokenBuilder().ReadFromConfiguration(jwtConfig).SetFromUserDto(user.User).Build());
        }

        [Route("logout")]
        [HttpPost]
        [Authorize]
        public Task LogoutAsync()
        {
            return Task.CompletedTask;
        }
    }
}
