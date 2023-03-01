using Hful.Core.Application;
using Hful.Domain.Iam;
using Hful.Domain;
using Hful.Iam.Api.Dto.Users;

namespace Hful.Iam.Service
{
    public interface IRoleService: ICurdService<Role, RoleDto, GetRoleListDto, SaveRoleDto, IRepository<Role>>
    {
        Task GrantPermissionAsync(Guid id, Guid permissionId);

         Task RevokePermissionAsync(Guid id, Guid permissionId);
        
         Task AssignPermissionAsync(Guid id, IEnumerable<Guid> permissionIds);
    }
}
