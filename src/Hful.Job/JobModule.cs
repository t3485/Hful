using Hful.Core;
using Hful.Core.Options;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Quartz;

using System.Reflection;

namespace Hful.Job
{
    public class JobModule : HfulModule
    {
        public override void PostConfigureServices(HfulModuleContext context)
        {
            using var provider = context.Services.BuildServiceProvider();

            var options = provider.GetService<IOptions<JobOptions>>();

            if (options != null && options.Value != null)
            {
                context.Services.AddQuartz(q =>
                {
                    foreach (var item in options.Value.JobTypes)
                    {
                        string key = GetStaticFieldString(item, "Key") ?? item.Name;
                        string? group = GetStaticFieldString(item, "Group");
                        string? cron = GetStaticFieldString(item, "Cron");

                        if (cron != null)
                        {
                            JobKey jobKey = string.IsNullOrEmpty(group) ? new JobKey(key) : new JobKey(key, group);
                            q.AddJob(item, jobKey);
                            q.AddTrigger(opts => opts
                                .ForJob(jobKey)
                                .WithIdentity(item.Name)
                                .WithCronSchedule(cron));
                        }
                    }
                    q.UseMicrosoftDependencyInjectionJobFactory();
                });

                context.Services.AddQuartzServer(options =>
                {
                    options.WaitForJobsToComplete = true;
                });
            }
        }

        private static string? GetStaticFieldString(Type type, string fieldName)
        {
            var field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
            return field?.GetValue(null)?.ToString();
        }
    }
}
