using Hful.File.Domain;

namespace Hful.File.Providers
{
    public interface IAttachmentProvider
    {
        string Name { get; }

        string Description { get; }

        Stream Download(Attachment attachment);

        Task<Stream> DownloadAsync(Attachment attachment);

        string GetAccessPath(Attachment attachment);

        void Upload(Attachment attachment, Stream stream);

        Task UploadAsync(Attachment attachment, Stream stream);

        Task DeleteAsync(Attachment attachment);
    }
}
