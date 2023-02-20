using Hful.Domain.Shared;

namespace Hful.Domain.Iam
{
    public class RolePermission : BaseEntity
    {
        public Guid RoleId { get; set; }

        public Guid PermissionId { get; set; }
    }
}
