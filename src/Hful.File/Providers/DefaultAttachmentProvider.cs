using Hful.File.Domain;

namespace Hful.File.Providers
{
    internal class DefaultAttachmentProvider : IAttachmentProvider
    {
        public string Name => "Default";

        public string Description => "";

        private readonly string basePath;
        private readonly string prefix;

        public Task DeleteAsync(Attachment attachment)
        {
            var path = GetAccessPath(attachment);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return Task.CompletedTask;
        }

        public Stream Download(Attachment attachment)
        {
            var path = GetAccessPath(attachment);
            if (System.IO.File.Exists(path))
            {
                return new FileStream(path, FileMode.Open);
            }

            return new MemoryStream();
        }

        public Task<Stream> DownloadAsync(Attachment attachment)
        {
            return Task.FromResult(Download(attachment));
        }

        public string GetAccessPath(Attachment attachment)
        {
            var path = attachment.Path;
            return Path.Combine(basePath, path);
        }

        public void Upload(Attachment attachment, Stream stream)
        {
            var path = GeneratePath(attachment);
            attachment.Path = path;

            using var file = new FileStream(path, FileMode.CreateNew);
            Span<byte> buffer = stackalloc byte[4096];
            while (stream.Read(buffer) != 0)
            {
                file.Write(buffer);
            }
        }

        public async Task UploadAsync(Attachment attachment, Stream stream)
        {
            var path = GeneratePath(attachment);
            attachment.Path = path;

            using var file = new FileStream(path, FileMode.CreateNew);
            byte[] buffer = new byte[4096];
            while (await stream.ReadAsync(buffer) != 0)
            {
                await file.WriteAsync(buffer);
            }
        }

        public async Task<bool> EqualsAsync(Stream x, Attachment y)
        {
            return false;
        }

        private string GeneratePath(Attachment attachment)
        {
            string path;

            if (attachment.TenantId != null)
            {
                path = Path.Combine(basePath,
                    attachment.TenantId.Value.ToString(),
                    DateTime.Now.ToString("yyyy-MM"),
                    Guid.NewGuid().ToString());
            }
            else
            {
                path = Path.Combine(basePath,
                   "default",
                   DateTime.Now.ToString("yyyy-MM"),
                   Guid.NewGuid().ToString());
            }

            return path;
        }
    }
}
