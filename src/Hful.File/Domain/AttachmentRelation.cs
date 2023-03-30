using Hful.Domain.Shared;

namespace Hful.File.Domain
{
    public class AttachmentRelation : AuditedEntity
    {
        public Guid AttachmentId { get; set; }

        public Guid BusinessId { get; set; }

        public string BusinessType { get; set; }

        public string? DisplayName { get;set; }

        public Guid? TenantId { get; set; }
    }
}
