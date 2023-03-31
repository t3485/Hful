using Hful.Core.Application;

namespace Hful.File.Dto
{
    public class AttachmentDto :IEntityDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Provider { get; set; }

        public int FileSize { get; set; }

        public Guid? TenantId { get; set; }
    }
}
