using System;
using System.Threading.Tasks;
using System.Timers;
using DotNetAppBase.Std.Exceptions.Assert;

// ReSharper disable UnusedMember.Global

namespace DotNetAppBase.Std.Library.Tasks;

public class TimerActionTask : ITask
{
    private readonly Timer _timer;

    private bool _disposed;

    public TimerActionTask(TimeSpan normalFrequence) : this(normalFrequence, normalFrequence, normalFrequence) { }

    public TimerActionTask(TimeSpan start, TimeSpan normalFrequence, TimeSpan errorFrequence)
    {
        NormalFrequence = normalFrequence;
        ErrorFrequence = errorFrequence;

        _timer = new Timer(start.TotalMilliseconds) {AutoReset = false};
    }

    public TimeSpan ErrorFrequence { get; set; }

    public TimeSpan NormalFrequence { get; set; }

    public bool AutoCatchException { get; set; }

    public bool Enabled => _timer.Enabled;

    public bool Start()
    {
        if (!_timer.Enabled)
        {
            _timer.Start();

            return true;
        }

        return false;
    }

    public async Task<bool> Stop(TimeSpan? timeout = null, bool waitComplete = false)
    {
        if (_timer.Enabled)
        {
            _timer.Stop();

            return await Task.Run(() => true);
        }

        return await Task.Run(() => false);
    }

    public event EventHandler<Exception> ExceptionHandled;

    public TimerActionTask Binding(Action action)
    {
        XContract.ArgIsNotNull(action, nameof(action));

        var localAction = action;

        _timer.Elapsed +=
            (sender, args) =>
                {
                    var restart = true;
                    try
                    {
                        localAction();

                        _timer.Interval = NormalFrequence.TotalMilliseconds;
                    }
                    catch (Exception ex)
                    {
                        XDebug.OnException(ex);

                        if (!AutoCatchException)
                        {
                            restart = false;

                            throw;
                        }

                        ExceptionHandled?.Invoke(this, ex);

                        _timer.Interval = ErrorFrequence.TotalMilliseconds;
                    }
                    finally
                    {
                        if (restart)
                        {
                            _timer.Start();
                        }
                    }
                };

        return this;
    }

    public static TimerActionTask New(TimeSpan start, TimeSpan normalFrequence, TimeSpan errorFrequence)
    {
        var newAutoSync = new TimerActionTask(start, normalFrequence, errorFrequence);

        return newAutoSync;
    }

    public void Reset()
    {
        if (!Enabled)
        {
            return;
        }

        _timer.Stop();
        _timer.Interval = NormalFrequence.TotalMilliseconds;
        _timer.Start();
    }

    #region IDisposable

    ~TimerActionTask()
    {
        Dispose(false);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        if (disposing)
        {
            GC.SuppressFinalize(this);
        }

        _timer.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    #endregion
}