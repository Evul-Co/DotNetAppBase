using System;
using System.Threading;

namespace DotNetAppBase.Std.Library.Tasks.Threading;

public class XTimeout
{
    private readonly DateTime _reference;
    private readonly TimeSpan _span;

    public XTimeout(TimeSpan span)
    {
        _reference = DateTime.Now;
        _span = span;
    }

    public bool IsZero => _span == TimeSpan.Zero;

    public bool Keep() => _reference + _span >= DateTime.Now;

    public bool WaitOne(WaitHandle waitHandle)
    {
        if (IsZero)
        {
            return waitHandle.WaitOne();
        }

        var diff = _span - (DateTime.Now - _reference);

        if (diff < TimeSpan.Zero)
        {
            diff = TimeSpan.FromMilliseconds(1);
        }

        return waitHandle.WaitOne(diff);
    }
}