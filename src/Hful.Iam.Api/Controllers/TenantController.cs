 using Hful.Core.Application;
using Hful.Iam.Api.Dto.Users;
using Hful.Iam.Api.Permissions;
using Hful.Iam.Dto;
using Hful.Iam.Service;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hful.Iam.Api.Controllers
{
    [ApiController]
    [Route("iam/tenant")]
    [Authorize]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [Authorize(TenantPermissionConstant.Tenant)]
        [HttpGet]
        [Route("list")]
        public Task<PageDto<TenantDto>> GetListAsync([FromQuery] GetTenantListDto dto)
        {
            return _tenantService.GetListAsync(dto);
        }

        [Authorize(TenantPermissionConstant.TenantSave)]
        [HttpPost]
        [Route("save")]
        public Task SaveAsync([FromBody] SaveTenantDto dto)
        {
            return _tenantService.SaveUserAsync(dto);
        }

        [Authorize(TenantPermissionConstant.TenantDelete)]
        [HttpDelete]
        [Route("del")]
        public Task DeleteAsync([FromQuery] Guid id)
        {
            return _tenantService.DeleteAsync(id);
        }
    }
}
