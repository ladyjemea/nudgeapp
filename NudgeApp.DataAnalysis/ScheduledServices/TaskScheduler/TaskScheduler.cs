namespace NudgeApp.DataAnalysis.ScheduledServices.TaskScheduler
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NCrontab;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class TimeTaskScheduler : IHostedService
    {
        private readonly IServiceScopeFactory ServiceScopeFactory;

        private readonly CancellationTokenSource cancelationTokenSource = new CancellationTokenSource();
        private Task executingTask;
        private CrontabSchedule schedule;

        protected abstract string Schedule { get; }

        public TimeTaskScheduler(IServiceScopeFactory serviceScopeFactory)
        {
            this.ServiceScopeFactory = serviceScopeFactory;

            this.schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
        }

        public abstract Task ScheduledTask(IServiceProvider serviceProvider);

        private async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var nextRun = this.schedule.GetNextOccurrence(DateTime.Now);
            do
            {
                if (DateTime.Now > nextRun)
                {
                    using (var scope = ServiceScopeFactory.CreateScope())
                    {
                        await this.ScheduledTask(scope.ServiceProvider);
                    }

                    nextRun = this.schedule.GetNextOccurrence(DateTime.Now);
                }

                await Task.Delay(5000, stoppingToken);
            } while (!stoppingToken.IsCancellationRequested);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.executingTask = ExecuteAsync(this.cancelationTokenSource.Token);

            if (this.executingTask.IsCompleted)
            {
                return this.executingTask;
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.cancelationTokenSource.Cancel();

            return Task.CompletedTask;
        }
    }
}
