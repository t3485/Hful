using Hful.File.BusinessProvider;
using Hful.File.Dto;
using Hful.File.Providers;

namespace Hful.File.Service
{
    public interface IAttachmentService
    {
        Task<AttachmentUploadDto> UploadFileAsync(Stream stream, string name, Guid? tenantId);

        Task<AttachmentUploadDto> UploadFileAsync(Stream stream, string name, Guid? tenantId, IAttachmentProvider provider);

        Task AddRelationAsync(Guid verifyKey, IBusinessProvider provider);

        Task AddRelationAsync(Guid verifyKey, IBusinessProvider provider, string? displayName);

        Task AddRelationAsync(IEnumerable<Guid> verifyKey, IBusinessProvider provider);

        Task DelRelationAsync(IBusinessProvider provider);

        Task DelAttachmentAsync(Guid id);

        Task DownloadFileAsync(Guid attachmentId);

        Task DownloadFileAsync(Guid attachmentId, IBusinessProvider provider);

        Task<List<AttachmentDto>> GetAsync(IBusinessProvider provider);
    }
}
