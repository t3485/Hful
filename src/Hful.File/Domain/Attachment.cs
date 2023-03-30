using Hful.Domain.Shared;

namespace Hful.File.Domain
{
    public class Attachment : AuditedEntity
    {
        public string Provider { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public int FileSize { get; set; }

        public string Hash { get; set; }

        public Guid? TenantId { get; set; }
    }
}
