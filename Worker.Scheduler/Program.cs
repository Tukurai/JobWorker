using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Uwp.Notifications;
using Worker.Infrastructure.Repositories;
using Worker.Scheduler.Services;

namespace Worker.Scheduler;

public class Program
{
    private static async void Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<SchedulerService>();
                services.AddScoped<IScheduledJobRepository, ScheduledJobRepository>();

                // Register other dependencies for the scheduler here  
                // e.g. services.AddSingleton<ISomeDependency, SomeDependencyImplementation>();  
            })
            .Build();

        await host.RunAsync();
    }
}