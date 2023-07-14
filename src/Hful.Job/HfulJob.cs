using Quartz;

namespace Hful.Job
{
    [DisallowConcurrentExecution]
    public abstract class HfulJob : IJob
    {
        public abstract Task Execute(HfulJobContext context);

        Task IJob.Execute(IJobExecutionContext context)
        {
            HfulJobContext hfulJobContext = new ();
            return Execute(hfulJobContext);
        }
    }
}
