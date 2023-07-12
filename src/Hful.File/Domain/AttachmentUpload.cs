using Hful.Domain.Shared;

namespace Hful.File.Domain
{
    public class AttachmentUpload : BaseEntity
    {
        public Guid AttachmentId { get; set; }

        public DateTime ExpiredTime { get; set; }

        public string? Name { get; set; }

        public Guid? TenantId { get; set; }
    }
}
