using Quartz;

namespace Hful.Job
{
    public abstract class HfulJob : IJob
    {
        public abstract Task Execute(HfulJobContext context);

        public abstract string Cron { get; }

        public abstract string Key { get; }

        Task IJob.Execute(IJobExecutionContext context)
        {
            HfulJobContext hfulJobContext = new();
            return Execute(hfulJobContext);
        }
    }
}
