using Hful.Core.Application;
using Hful.Domain;
using Hful.Iam.Domain;
using Hful.Iam.Dto;

namespace Hful.Iam.Service
{
    public interface ITenantService : ICurdService<Tenant, TenantDto, GetTenantListDto, SaveTenantDto, IRepository<Tenant>>
    {
    }
}
