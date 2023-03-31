namespace Hful.File.Dto
{
    public class AttachmentUploadDto
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string Provider { get; set; }

        public int FileSize { get; set; }

        public Guid? TenantId { get; set; }

        public Guid VerifyKey { get; set; }
    }
}
