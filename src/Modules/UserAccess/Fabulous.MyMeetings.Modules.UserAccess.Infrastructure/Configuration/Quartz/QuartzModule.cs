using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.Inbox;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.InternalCommands;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Quartz;

internal static class QuartzModule
{
    public static void AddQuartz(this IServiceCollection services,
        long? internalProcessingPoolingInterval = null)
    {
        services.Configure<QuartzOptions>(opts =>
        {
            opts.Scheduling.IgnoreDuplicates = true;
            opts.Scheduling.OverWriteExistingData = true;
        });

        services.AddQuartz(q =>
        {
            q.SchedulerId = "UserAccessModuleScheduler";
            q.SchedulerName = "User Access Module Scheduler";
            q.InterruptJobsOnShutdown = true;
            q.InterruptJobsOnShutdownWithWait = true;
            q.UseTimeZoneConverter();

            var processOutboxJobKey = new JobKey("ProcessOutboxJob");
            q.AddJob<ProcessOutboxJob>(b => b
                .StoreDurably()
                .WithIdentity(processOutboxJobKey)
                .RequestRecovery()
                .WithDescription("Process Outbox Messages"));

            if (internalProcessingPoolingInterval.HasValue)
                q.AddTrigger(cfg => cfg
                    .WithIdentity("ProcessOutboxJobTrigger")
                    .StartNow()
                    .ForJob(processOutboxJobKey)
                    .WithSimpleSchedule(s => s
                        .WithInterval(TimeSpan.FromMilliseconds(internalProcessingPoolingInterval.Value))
                        .RepeatForever())
                );
            else
                q.AddTrigger(cfg => cfg
                    .WithIdentity("ProcessOutboxJobTrigger")
                    .StartNow()
                    .ForJob(processOutboxJobKey)
                    .WithCronSchedule("0/2 * * ? * *")
                );

            var processInboxJobKey = new JobKey("ProcessInboxJob");
            q.AddJob<ProcessInboxJob>(b => b
                .StoreDurably()
                .WithIdentity(processInboxJobKey)
                .RequestRecovery()
                .WithDescription("Process Inbox Messages"));

            if (internalProcessingPoolingInterval.HasValue)
                q.AddTrigger(cfg => cfg
                    .WithIdentity("ProcessInboxJobTrigger")
                    .StartNow()
                    .ForJob(processInboxJobKey)
                    .WithSimpleSchedule(s => s
                        .WithInterval(TimeSpan.FromMilliseconds(internalProcessingPoolingInterval.Value))
                        .RepeatForever())
                );
            else
                q.AddTrigger(cfg => cfg
                    .WithIdentity("ProcessInboxJobTrigger")
                    .StartNow()
                    .ForJob(processInboxJobKey)
                    .WithCronSchedule("0/2 * * ? * *")
                );

            var processInternalCommandsJobKey = new JobKey("processInternalCommandsJob");
            q.AddJob<ProcessInternalCommandsJob>(b => b
                .StoreDurably()
                .WithIdentity(processInternalCommandsJobKey)
                .RequestRecovery()
                .WithDescription("Process Internal Command Messages"));

            if (internalProcessingPoolingInterval.HasValue)
                q.AddTrigger(cfg => cfg
                    .WithIdentity("ProcessInternalCommandsJobTrigger")
                    .StartNow()
                    .ForJob(processInternalCommandsJobKey)
                    .WithSimpleSchedule(s => s
                        .WithInterval(TimeSpan.FromMilliseconds(internalProcessingPoolingInterval.Value))
                        .RepeatForever())
                );
            else
                q.AddTrigger(cfg => cfg
                    .WithIdentity("ProcessInternalCommandsJobTrigger")
                    .StartNow()
                    .ForJob(processInternalCommandsJobKey)
                    .WithCronSchedule("0/2 * * ? * *")
                );

            //q.UseJobAutoInterrupt(cfg => cfg.DefaultMaxRunTime = TimeSpan.FromHours(3));

            //q.UsePersistentStore(store =>
            //{
            //    store.PerformSchemaValidation = true;
            //    store.UseProperties = true;
            //    store.UseClustering();
            //    store.UseSqlServer(cfg =>
            //    {
            //        cfg.ConnectionString = connectionString;
            //        cfg.TablePrefix = "Users.QRTZ_";
            //    });
            //    store.UseSystemTextJsonSerializer();
            //});
        });

        services.AddQuartzHostedService(opts => { opts.WaitForJobsToComplete = false; });
    }
}