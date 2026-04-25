using System;
using System.Threading.Tasks;

namespace DotNetAppBase.Std.Library.Async;

public class AsyncExecutor<TData>
{
    private readonly Func<TData> _execute;

    private readonly object _sync = new object();

    private Task _task;
    private TData _value;

    public AsyncExecutor(Func<TData> execute, bool autoBeginInvoke = true)
    {
        _execute = execute;

        if (autoBeginInvoke)
        {
            BeginInvoke();
        }
    }

    public bool BeginInvoke()
    {
        lock (_sync)
        {
            if (_task != null)
            {
                return false;
            }

            _task = Task.Run(
                () =>
                    {
                        _value = _execute();

                        _task = null;
                    });

            return true;
        }
    }

    public TData ReadValue(int milisecondsTimeout = int.MaxValue)
    {
        lock (_sync)
        {
            _task?.Wait(milisecondsTimeout);

            return _value;
        }
    }
}