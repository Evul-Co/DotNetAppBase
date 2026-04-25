using System;

namespace DotNetAppBase.Std.Library.Concurrency;

public class XDo
{
    private readonly Action<object> _action;
    private readonly object _sync = new object();

    private bool _did;

    public XDo(Action<object> action)
    {
        _action = action;
    }

    public void Do(object args)
    {
        lock (_sync)
        {
            if (_did)
            {
                return;
            }

            _did = true;

            _action(args);
        }
    }

    public void Reset()
    {
        lock (_sync)
        {
            if (!_did)
            {
                return;
            }

            _did = false;
        }
    }
}