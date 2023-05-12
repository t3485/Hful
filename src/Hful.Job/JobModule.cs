using Hful.Core;

using Quartz;

namespace Hful.Job
{
    public class JobModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
            context.Services.AddQuartz(q =>
            {
                // base Quartz scheduler, job and trigger configuration
            });

            context.Services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });
        }
    }
}
