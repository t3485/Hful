using Hful.Core.UnitOfWork;
using Hful.Domain;
using Hful.File.Domain;
using Hful.File.Service;
using Hful.Job;

namespace Hful.File.Job
{
    public class CleanJob : HfulJob
    {
        public const string Key = "";
        public const string Group = "";
        public const string Cron = "";

        private readonly IAttachmentService attachmentService;
        private readonly IRepository<AttachmentUpload> _uploadRepository;
        private readonly IUowManager _uowManager;

        public CleanJob(IAttachmentService attachmentService, IRepository<AttachmentUpload> uploadRepository, IUowManager uowManager)
        {
            this.attachmentService = attachmentService;
            _uploadRepository = uploadRepository;
            _uowManager = uowManager;
        }

        public async override Task Execute(HfulJobContext context)
        {
            var uploads = _uploadRepository.AsQueryable().Where(x => x.ExpiredTime >= DateTime.Now);

            // todo double check
            foreach (var item in uploads)
            {
                try
                {
                    await attachmentService.DelAttachmentAsync(item.AttachmentId);
                }
                catch
                {
                    // ignore
                }
                await _uploadRepository.DeleteAsync(item.Id);
            }
        }
    }
}