using Hful.Iam.Api.Attributes;
using Hful.Iam.Api.Dto.Authorization;
using Hful.Iam.Service;
using Hful.Iam.Util;

using Lazy.Captcha.Core;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hful.Iam.Api.Controllers
{
    [ApiController]
    [Route("iam/auth")]
    [ResponseWrapper]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginService _loginService;
        private readonly ICaptcha _captcha;
        private readonly IPermissionService _permissionService;
        private readonly ICurrentUser _currentUser;

        public AuthorizationController(IConfiguration configuration,
            ILoginService loginService,
            ICaptcha captcha,
            IPermissionService permissionService,
            ICurrentUser currentUser)
        {
            _configuration = configuration;
            _loginService = loginService;
            _captcha = captcha;
            _permissionService = permissionService;
            _currentUser = currentUser;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync([FromBody] LoginRequestDto dto)
        {
            var data = new LoginDto();

            var user = await _loginService.LoginAsync(dto.Username, dto.Password);
            if (!user.Status || user.User == null)
            {
                return Unauthorized(data);
            }

            var permissions = await _permissionService.GetPermissionAsync(null, user.User.Id);

            var jwtConfig = _configuration.GetSection("Jwt");
            data.Token = new TokenBuilder().ReadFromConfiguration(jwtConfig).SetFromUserDto(user.User).SetPermissions(permissions).Build();

            return new ObjectResult(data);
        }

        [Route("logout")]
        [HttpPost]
        [Authorize]
        public Task LogoutAsync()
        {
            return Task.CompletedTask;
        }

        [Route("current")]
        [HttpGet]
        [Authorize]
        public CurrentUserDto GetCurrentUserAsync()
        {
            CurrentUserDto dto = new CurrentUserDto();
            dto.Id = _currentUser.Id;

            return dto;
        }

        [Route("code")]
        [HttpGet]
        [AllowAnonymous]
        public string CodeAsync(string captachId)
        {
            var info = _captcha.Generate(captachId);
            return Convert.ToBase64String(info.Bytes);
        }
    }
}
