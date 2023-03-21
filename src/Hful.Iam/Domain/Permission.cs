using Hful.Domain.Shared;

namespace Hful.Domain.Iam
{
    public class Permission : AuditedEntity
    {
        public int MenuId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Guid? TenantId { get; set; }
    }
}
