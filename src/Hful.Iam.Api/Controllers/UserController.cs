using Hful.Application.Contracts;
using Hful.Domain;
using Hful.Domain.Iam;
using Hful.Iam.Api.Dto.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hful.Iam.Api.Controllers
{
    [ApiController]
    [Route("iam/user")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IAsyncExecutor _asyncExecutor;

        public UserController(IRepository<User> userRepository, IAsyncExecutor asyncExecutor)
        {
            _userRepository = userRepository;
            _asyncExecutor = asyncExecutor;
        }

        [Authorize(AuthorizationPermission.User)]
        [HttpGet]
        [Route("list")]
        public async Task<PageDto<UserDto>> GetListAsync([FromQuery] GetUserListDto dto)
        {
            var data = await _asyncExecutor.ToListAsync(_userRepository.AsQueryable());
            return new PageDto<UserDto>(data.Select(x => new UserDto()));
        }

        [Authorize(AuthorizationPermission.UserSave)]
        [HttpPost]
        [Route("save")]
        public async Task SaveAsync([FromBody] SaveUserDto dto)
        {
        }

        [Authorize(AuthorizationPermission.UserDelete)]
        [HttpDelete]
        [Route("del")]
        public async Task DeleteAsync(Guid id)
        {
        }
    }
}
