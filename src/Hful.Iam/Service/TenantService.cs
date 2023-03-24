using Hful.Core.Application;
using Hful.Core.Mapper;
using Hful.Domain;
using Hful.Iam.Domain;
using Hful.Iam.Dto;

namespace Hful.Iam.Service
{
    internal class TenantService : CurdService<Tenant, TenantDto, GetTenantListDto, SaveTenantDto, IRepository<Tenant>>, ITenantService
    {
        public TenantService(IObjectMapper objectMapper, IRepository<Tenant> userRepository, IAsyncExecutor asyncExecutor) : base(objectMapper, userRepository, asyncExecutor)
        {
        }
    }
}