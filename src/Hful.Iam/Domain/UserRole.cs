using Hful.Domain.Shared;

namespace Hful.Domain.Iam
{
    public class UserRole : BaseEntity
    {
        public Guid RoleId { get; set; }

        public Guid UserId { get; set; }
    }
}
