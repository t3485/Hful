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

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [Route("menu")]
        [HttpGet]
        public async Task<List<MenuDto>> GetMenuAsync()
        {
            return await _permissionService.GetMenu(Guid.Empty, Guid.Empty);
        }
    }
}
