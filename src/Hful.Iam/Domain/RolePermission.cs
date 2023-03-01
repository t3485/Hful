using Hful.Domain.Shared;

namespace Hful.Domain.Iam
{
    public class RolePermission : BaseEntity
    {
        public RolePermission()
        {
        }

        public RolePermission(Guid roleId, Guid permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }

        public Guid RoleId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
