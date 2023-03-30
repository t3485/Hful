using Hful.File.Domain;

namespace Hful.File.Providers
{
    internal class DefaultAttachmentProvider : IAttachmentProvider
    {
        public string Name => "Default";

        public string Description => "";

        public Stream Download(Attachment attachment)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> DownloadAsync(Attachment attachment)
        {
            throw new NotImplementedException();
        }

        public string GetAccessPath(Attachment attachment)
        {
            throw new NotImplementedException();
        }

        public void Upload(Attachment attachment, Stream stream)
        {
            throw new NotImplementedException();
        }

        public Task UploadAsync(Attachment attachment, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
