using Hful.Application.Contracts;
using Hful.Domain;
using Hful.Domain.Iam;
using Hful.Iam.Api.Dto.Users;
using Hful.Iam.Service;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hful.Iam.Api.Controllers
{
    [ApiController]
    [Route("iam/user")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUserService _userService;
        private readonly IAsyncExecutor _asyncExecutor;

        public UserController(IRepository<User> userRepository, IAsyncExecutor asyncExecutor, IUserService userService)
        {
            _userRepository = userRepository;
            _asyncExecutor = asyncExecutor;
            _userService = userService;
        }

        [Authorize(AuthorizationPermission.User)]
        [HttpGet]
        [Route("list")]
        public Task<PageDto<UserDto>> GetListAsync([FromQuery] GetUserListDto dto)
        {
            return _userService.GetListAsync(dto);
        }

        [Authorize(AuthorizationPermission.UserSave)]
        [HttpPost]
        [Route("save")]
        public Task SaveAsync([FromBody] SaveUserDto dto)
        {
            // todo check
            return _userService.SaveUserAsync(dto);
        }

        [Authorize(AuthorizationPermission.UserDelete)]
        [HttpDelete]
        [Route("del")]
        public Task DeleteAsync([FromQuery] Guid id)
        {
            return _userService.DeleteAsync(id);
        }
    }
}
