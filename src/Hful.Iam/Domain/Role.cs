using Hful.Domain.Shared;

namespace Hful.Domain.Iam
{
    public class Role : AuditedEntity, ITenant
    {
        public string Code { get;set; }

        public string Name { get;set; }

        public Guid? TenantId { get; set; }
    }
}
