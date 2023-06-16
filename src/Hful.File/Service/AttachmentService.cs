using Hful.Core.Mapper;
using Hful.Core.UnitOfWork;
using Hful.Domain;
using Hful.File.BusinessProvider;
using Hful.File.Configuration;
using Hful.File.Domain;
using Hful.File.Dto;
using Hful.File.Providers;

namespace Hful.File.Service
{
    internal class AttachmentService : IAttachmentService
    {
        private readonly IRepository<Attachment> _attachmentRepository;
        private readonly IRepository<AttachmentRelation> _relationRepository;
        private readonly IRepository<AttachmentUpload> _uploadRepository;
        private readonly IUowManager _uowManager;
        private readonly IObjectMapper _objectMapper;
        private readonly IAsyncExecutor _asyncExecutor;

        public AttachmentService(IRepository<Attachment> attachmentRepository,
            IRepository<AttachmentRelation> relationRepository,
            IRepository<AttachmentUpload> uploadRepository,
            IUowManager uowManager,
            IObjectMapper objectMapper,
            IAsyncExecutor asyncExecutor)
        {
            _attachmentRepository = attachmentRepository;
            _relationRepository = relationRepository;
            _uploadRepository = uploadRepository;
            _uowManager = uowManager;
            _objectMapper = objectMapper;
            _asyncExecutor = asyncExecutor;
        }

        public Task<AttachmentUploadDto> UploadFileAsync(Stream stream, string name, Guid? tenantId)
        {
            return UploadFileAsync(stream, name, tenantId, AttachmentConfiguration.DefaultProvider);
        }

        public async Task<AttachmentUploadDto> UploadFileAsync(Stream stream, string name, Guid? tenantId, IAttachmentProvider provider)
        {
            using var uow = _uowManager.Begin();

            Attachment attachment = new Attachment();
            attachment.Provider = provider.Name;
            attachment.Name = name;
            attachment.TenantId = tenantId;

            try
            {
                await provider.UploadAsync(attachment, stream);
                await _attachmentRepository.SaveAsync(attachment);

                var upload = new AttachmentUpload
                {
                    AttachmentId = attachment.Id
                };
                await _uploadRepository.SaveAsync(upload);

                var result = _objectMapper.Map<Attachment, AttachmentUploadDto>(attachment);
                result.VerifyKey = upload.Id;

                await uow.SaveChangesAsync();
                await uow.CompleteAsync();

                return result;
            }
            catch
            {
                try
                {
                    await provider.DeleteAsync(attachment);
                }
                catch
                {
                    // ignore
                }
                throw;
            }
        }

        public Task AddRelationAsync(Guid verifyKey, IBusinessProvider provider)
        {
            return AddRelationAsync(verifyKey, provider, null);
        }

        public async Task AddRelationAsync(Guid verifyKey, IBusinessProvider provider, string? displayName)
        {
            using var uow = _uowManager.Begin();

            // todo 原子操作
            var upload = await _uploadRepository.FindByIdAsync(verifyKey);
            if (upload == null)
            {
                throw new InvalidOperationException();
            }
            await _uploadRepository.DeleteAsync(upload.Id);

            var attachment = await _attachmentRepository.FindByIdAsync(upload.AttachmentId);
            if (attachment == null)
            {
                throw new InvalidOperationException();
            }

            if (attachment.TenantId != provider.TenantId)
            {
                throw new InvalidOperationException();
            }

            var relation = new AttachmentRelation
            {
                AttachmentId = upload.AttachmentId,
                BusinessId = provider.Id,
                BusinessType = provider.Type,
                TenantId = attachment.TenantId,
                DisplayName = displayName
            };
            await _relationRepository.SaveAsync(relation);

            await uow.SaveChangesAsync();
            await uow.CompleteAsync();
        }

        public async Task AddRelationAsync(IEnumerable<Guid> verifyKey, IBusinessProvider provider)
        {
            using var uow = _uowManager.Begin();

            var upload = await _uploadRepository.FindByIdAsync(verifyKey);
            await _uploadRepository.DeleteAsync(upload.Select(x => x.AttachmentId).ToList());

            var attachment = await _attachmentRepository.FindByIdAsync(upload.Select(x => x.AttachmentId));

            foreach (var item in attachment)
            {
                if (item.TenantId != provider.TenantId)
                {
                    throw new InvalidOperationException();
                }
            }

            var relation = attachment.Select(x => new AttachmentRelation
            {
                AttachmentId = x.Id,
                BusinessId = provider.Id,
                BusinessType = provider.Type,
                TenantId = x.TenantId
            }).ToList();
            await _relationRepository.SaveAsync(relation);

            await uow.SaveChangesAsync();
            await uow.CompleteAsync();
        }

        public async Task DelRelationAsync(IBusinessProvider provider)
        {
            var data = await _asyncExecutor.ToListAsync(
                _relationRepository.AsQueryable().Where(x => x.BusinessType == provider.Type && x.BusinessId == provider.Id && x.TenantId == provider.TenantId));
            await _relationRepository.DeleteAsync(data.Select(x => x.Id));
        }

        public async Task DelAttachmentAsync(Guid attachmentId)
        {
            using var uow = _uowManager.Begin();

            var attachmnet = await _attachmentRepository.FindByIdAsync(attachmentId);
            if (attachmnet == null)
            {
                throw new InvalidOperationException();
            }
            await _attachmentRepository.DeleteAsync(attachmentId);

            var relation = await _asyncExecutor.ToListAsync(_relationRepository.AsQueryable().Where(x => x.AttachmentId == attachmentId));
            await _relationRepository.DeleteAsync(relation.Select(x => x.Id));

            var upload = await _asyncExecutor.ToListAsync(_uploadRepository.AsQueryable().Where(x => x.AttachmentId == attachmentId));
            await _uploadRepository.DeleteAsync(upload.Select(x => x.Id));

            var provider = AttachmentConfiguration.GetProvider(attachmnet.Provider);
            if (provider == null)
            {
                throw new InvalidOperationException();
            }
            await provider.DeleteAsync(attachmnet);

            await uow.SaveChangesAsync();
            await uow.CompleteAsync();
        }

        public async Task<DownloadFileDto?> DownloadFileAsync(Guid attachmentId)
        {
            var attachment = await _attachmentRepository.FindByIdAsync(attachmentId);
            if (attachment == null)
            {
                return null;
            }

            var provider = AttachmentConfiguration.GetProvider(attachment.Provider);
            if (provider == null)
            {
                return null;
            }

            DownloadFileDto dto = new()
            {
                Stream = await provider.DownloadAsync(attachment),
                Extension = Path.GetExtension(attachment.Name),
                Name = attachment.Name
            };
            return dto;
        }

        public async Task<List<AttachmentDto>> GetAsync(IBusinessProvider provider)
        {
            var data = await _asyncExecutor.ToListAsync(
                _relationRepository.AsQueryable().Where(x => x.BusinessType == provider.Type && x.BusinessId == provider.Id && x.TenantId == provider.TenantId));

            var attachment = await _attachmentRepository.FindByIdAsync(data.Select(x => x.Id));
            return _objectMapper.Map<List<Attachment>, List<AttachmentDto>>(attachment);
        }
    }
}
