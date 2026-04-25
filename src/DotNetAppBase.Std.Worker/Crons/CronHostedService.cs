using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using DotNetAppBase.Std.Worker.Base;
using Microsoft.Extensions.Logging;
using Timer = System.Timers.Timer;

namespace DotNetAppBase.Std.Worker.Crons
{
    public abstract class CronHostedService : HostedServiceBase
    {
        private readonly string _cronExpression;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;

        private Timer _timer;

        protected CronHostedService(string cronExpression, TimeZoneInfo timeZoneInfo, ILoggerFactory loggerFactory)
        {
            _cronExpression = cronExpression;

            _expression = CronExpression.Parse(cronExpression);
            _timeZoneInfo = timeZoneInfo;

            Logger = loggerFactory.CreateLogger($"{GetType().Name}#{Name}");
        }

        protected override void InternalDisposing(in bool disposing) => _timer?.Dispose();

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation($"Service starting (Cron: {_cronExpression})");

            await ScheduleJob(cancellationToken);
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();

            await Task.CompletedTask;
        }

        public abstract Task<Result> DoWork(CancellationToken cancellationToken);

        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;

                _timer = new Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) =>
                    {
                        _timer.Dispose();
                        _timer = null;

                        while (!cancellationToken.IsCancellationRequested)
                        {
                            var result = await DoWork(cancellationToken);
                            if (result.Success || result.RetryTimeSpan == null)
                            {
                                break;
                            }
                        }

                        if (!cancellationToken.IsCancellationRequested)
                        {
                            await ScheduleJob(cancellationToken);
                        }
                    };

                _timer.Start();

                Logger.LogWarning($"Next occurrence at {next.Value:dd/MM/yyyy hh:mm:ss}");
            }

            await Task.CompletedTask;
        }

        public class Result
        {
            public TimeSpan? RetryTimeSpan { get; set; }
            public bool Success { get; set; }
        }
    }
}