using System;
using System.Threading;
using System.Threading.Tasks;
using DotNetAppBase.Std.Worker.Base;

namespace DotNetAppBase.Std.Worker.Timed
{
    public abstract class TimedHostedService : HostedServiceBase
    {
        private Timer _timer;

        public TimeSpan BeginOn { get; } = TimeSpan.Zero;

        public abstract TimeSpan Period { get; }

        protected abstract void DoWork(object state);

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, BeginOn, Period);

            return Task.CompletedTask;
        }

        protected override Task InternalCleanup(Task obj)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return base.InternalCleanup(obj);
        }

        protected override void InternalDisposing(in bool disposing)
        {
            base.InternalDisposing(in disposing);

            _timer?.Dispose();
        }
    }
}