using Hful.Iam.Dto;
using Hful.Iam.Service;
using Hful.Iam.Util;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Hful.Iam.Api.Controllers
{
    [ApiController]
    [Route("iam/permission")]
    [Authorize]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        private readonly ICurrentUser _currentUser;

        public PermissionController(IPermissionService permissionService, ICurrentUser currentUser)
        {
            _permissionService = permissionService;
            _currentUser = currentUser;
        }

        [Route("menu")]
        [HttpGet]
        public async Task<List<MenuDto>> GetMenuAsync()
        {
            return await _permissionService.GetMenu(_currentUser.TenantId, _currentUser.Id);
        }
    }
}
