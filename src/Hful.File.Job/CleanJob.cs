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

        public CleanJob(IAttachmentService attachmentService)
        {
            this.attachmentService = attachmentService;
        }

        public async override Task Execute(HfulJobContext context)
        {

        }
    }
}