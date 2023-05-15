using Quartz;

namespace Hful.Job
{
    public abstract class HfulJob : IJob
    {
        public string? Cron { get; }

        public abstract Task Execute(HfulJobContext context);

        Task IJob.Execute(IJobExecutionContext context)
        {
            HfulJobContext hfulJobContext = new ();
            return Execute(hfulJobContext);
        }
    }
}
