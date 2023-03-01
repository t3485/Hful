using Hful.Domain.Iam;
using Hful.Domain;
using Hful.Iam.Api.Dto.Users;
using Hful.Core.Mapper;
using Hful.Core.Application;

namespace Hful.Iam.Service
{
    internal class RoleService : CurdService<Role, RoleDto, GetRoleListDto, SaveRoleDto, IRepository<Role>>, IRoleService
    {
        private IRepository<RolePermission> _rolePermissionRepository;

        public RoleService(IObjectMapper objectMapper,
            IRepository<Role> roleRepository,
            IAsyncExecutor asyncExecutor,
            IRepository<RolePermission> rolePermissionRepository)
            : base(objectMapper, roleRepository, asyncExecutor)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task GrantPermissionAsync(Guid id, Guid permissionId)
        {
            var p = await _asyncExecutor.FirstOrDefaultAsync(_rolePermissionRepository.AsQueryable().Where(x => x.RoleId == id && x.PermissionId == permissionId));
            if (p == null)
            {
                await _rolePermissionRepository.SaveAsync(new RolePermission(id, permissionId));
            }
        }

        public async Task RevokePermissionAsync(Guid id, Guid permissionId)
        {
            var p = await _asyncExecutor.FirstOrDefaultAsync(_rolePermissionRepository.AsQueryable().Where(x => x.RoleId == id && x.PermissionId == permissionId));
            if (p != null)
            {
                await _rolePermissionRepository.DeleteAsync(p.Id);
            }
        }

        public async Task AssignPermissionAsync(Guid id, IEnumerable<Guid> permissionIds)
        {
            var p = await _asyncExecutor.ToListAsync(_rolePermissionRepository.AsQueryable().Where(x => x.RoleId == id));
            var pId = p.Select(x => x.Id).ToHashSet();

            await using var tran = await _rolePermissionRepository.BeginTransactionAsync();

            await _rolePermissionRepository.SaveAsync(permissionIds.Except(pId).Select(x => new RolePermission(id, x)).ToList());
            await _rolePermissionRepository.DeleteAsync(pId.Except(permissionIds).ToList());
        }
    }
}
