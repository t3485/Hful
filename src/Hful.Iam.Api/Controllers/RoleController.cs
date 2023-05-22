using Hful.Core.Application;
using Hful.Iam.Api.Dto.Users;
using Hful.Iam.Api.Permissions;
using Hful.Iam.Service;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hful.Iam.Api.Controllers
{
    [ApiController]
    [Route("iam/role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(RolePermissionConstant.Role)]
        [HttpGet]
        [Route("list")]
        public Task<PageDto<RoleDto>> GetListAsync([FromQuery] GetRoleListDto dto)
        {
            return _roleService.GetListAsync(dto);
        }

        [Authorize(RolePermissionConstant.RoleSave)]
        [HttpPost]
        [Route("save")]
        public Task SaveAsync([FromBody] SaveRoleDto dto)
        {
            return _roleService.SaveUserAsync(dto);
        }

        [Authorize(RolePermissionConstant.RoleDelete)]
        [HttpDelete]
        [Route("del")]
        public Task DeleteAsync([FromQuery] Guid id)
        {
            return _roleService.DeleteAsync(id);
        }
    }
}
