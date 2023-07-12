using Hful.Core;
using Hful.Job;

namespace Hful.File.Job
{
    [HfulDependOn(typeof(FileModule), typeof(JobModule))]
    public class FileJobModule : HfulModule
    {
        public override void ConfigureServices(HfulModuleContext context)
        {
            Config<JobOptions>(x =>
            {
                x.AddJob<CleanJob>();
            });
        }
    }
}
