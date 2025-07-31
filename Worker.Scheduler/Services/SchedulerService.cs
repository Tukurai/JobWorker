using Cronos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Uwp.Notifications;
using Worker.Core.Models;

namespace Worker.Scheduler.Services;

public class SchedulerService(ILogger<SchedulerService> logger) : BackgroundService()
{
    private readonly List<CancellationTokenSource> _jobCancellationTokens = [];

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Scheduler service is starting");

        new ToastContentBuilder()
            .AddText("Scheduler Started")
            .AddText("The Worker.Scheduler service has started successfully.")
            .Show();

        LoadPresetCronJobs();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation("Scheduler tick at: {time}", DateTimeOffset.Now);

                // Simulate work - replace with real scheduling work

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
            catch (OperationCanceledException)
            {
                // Expected when stoppingToken is canceled, do nothing
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred executing scheduler.");
            }

            logger.LogInformation("Scheduler Service is stopping.");

            new ToastContentBuilder()
                .AddText("Scheduler Stopped")
                .AddText("The Worker.Scheduler service has stopped.")
                .Show();
        }
    }

    private void LoadPresetCronJobs()
    {
        // Load preset cron jobs from configuration or database
        // This is a placeholder for actual implementation
        // Example: logger.LogInformation("Loading preset cron jobs...");
        // Simulate loading jobs
        logger.LogInformation("Preset cron jobs loaded successfully.");
        
        new ToastContentBuilder()
            .AddText("Preset Jobs Loaded")
            .AddText("Preset cron jobs have been loaded successfully.")
            .Show();
    }

    private void ScheduleJob(ScheduledJob job, CancellationToken stoppingToken)
    {
        var schedule = CronExpression.Parse(job.CronExpression, CronFormat.IncludeSeconds);

        var cts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
        _jobCancellationTokens.Add(cts);

        Task.Run(async () =>
        {
            logger.LogInformation("Scheduled job {JobId} started.", job.Id);

            while (!cts.Token.IsCancellationRequested)
            {
                var nextUtc = schedule.GetNextOccurrence(DateTime.UtcNow, inclusive: false);
                if (!nextUtc.HasValue)
                {
                    logger.LogWarning("No further occurrences for job {JobId}. Ending scheduling.", job.Id);
                    break;
                }

                var delay = nextUtc.Value - DateTime.UtcNow;
                if (delay.TotalMilliseconds <= 0) // If next occurrence time is in the past, skip waiting
                    continue;

                try
                {
                    await Task.Delay(delay, cts.Token);
                }
                catch (TaskCanceledException) // Cancellation requested, exit loop
                {
                    break;
                }

                if (!cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        logger.LogInformation("Executing scheduled job {JobId} at {Time}", job.Id, DateTimeOffset.Now);
                        await ExecuteJobAsync(job);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error executing scheduled job {JobId}", job.Id);
                    }
                }
            }

            logger.LogInformation("Scheduled job {JobId} stopped.", job.Id);
        }, cts.Token);
    }
    private Task ExecuteJobAsync(ScheduledJob job)
    {
        // TODO: Implement job execution logic based on your domain  
        logger.LogInformation("Job {JobId} executed (placeholder).", job.Id);
        return Task.CompletedTask;
    }

    public void CancelAllJobs()
    {
        foreach (var cts in _jobCancellationTokens)
        {
            cts.Cancel();
        }
    }
}