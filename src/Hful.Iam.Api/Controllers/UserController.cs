using Hful.Core.Application;
using Hful.Domain.Iam;
using Hful.Iam.Api.Permissions;
using Hful.Iam.Attributes;
using Hful.Iam.Dto;
using Hful.Iam.Service;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hful.Iam.Api.Controllers
{
    [ApiController]
    [Route("iam/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(UserPermissionConstant.User)]
        [HttpGet]
        [Route("list")]
        [DataFilter<User>(UserPermissionConstant.User)]
        public Task<PageDto<UserDto>> GetListAsync([FromQuery] GetUserListDto dto)
        {
            return _userService.GetListAsync(dto);
        }

        [Authorize(UserPermissionConstant.UserSave)]
        [HttpPost]
        [Route("save")]
        [DataFilter<User>(UserPermissionConstant.UserSave)]
        public Task SaveAsync([FromBody] SaveUserDto dto)
        {
            return _userService.SaveUserAsync(dto);
        }

        [Authorize(UserPermissionConstant.UserDelete)]
        [HttpDelete]
        [Route("del")]
        [DataFilter<User>(UserPermissionConstant.UserDelete)]
        public Task DeleteAsync([FromQuery] Guid id)
        {
            return _userService.DeleteAsync(id);
        }
    }
}
