using System;
using System.Timers;

// ReSharper disable UnusedMember.Global

namespace DotNetAppBase.Std.Library.Timers;

public class XTimer
{
    private readonly Timer _timer;

    public XTimer(double interval, bool autoReset, Action action, bool enabled = true)
    {
        _timer = new Timer(interval)
            {
                AutoReset = autoReset
            };
        _timer.Elapsed += (sender, args) => action();

        Enabled = enabled;
    }

    public XTimer(TimeSpan interval, bool autoReset, Action action) : this(interval.TotalMilliseconds, autoReset, action) { }

    public bool Enabled
    {
        get => _timer.Enabled;
        set => _timer.Enabled = value;
    }
}