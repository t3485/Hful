using Hful.Iam.Api.Attributes;
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
        private readonly ICurrentTenant _currentTenant;

        public PermissionController(IPermissionService permissionService, ICurrentUser currentUser, ICurrentTenant currentTenant)
        {
            _permissionService = permissionService;
            _currentUser = currentUser;
            _currentTenant = currentTenant;
        }

        [Route("menu")]
        [HttpGet]
        [ResponseWrapper]
        public async Task<List<MenuDto>> GetMenuAsync()
        {
            return await _permissionService.GetMenuAsync(_currentTenant.Id, _currentUser.Id.Value);
        }
    }
}
