using System;

namespace DotNetAppBase.Std.Library.Cache;

public class LifetimeCache<TValue>
{
    private readonly Func<TValue> _funcCreate;
    private readonly object _syncReadValue = new object();
    private DateTime _cached;

    private TValue _value;

    public LifetimeCache(long lifetime, Func<TValue> funcCreate)
    {
        _funcCreate = funcCreate;

        Lifetime = lifetime;
    }

    public long Lifetime { get; }

    public TValue Value
    {
        get
        {
            lock (_syncReadValue)
            {
                var now = DateTime.Now;

                if (_cached.AddMilliseconds(Lifetime) >= now)
                {
                    return _value;
                }

                _value = _funcCreate();
                _cached = now;

                return _value;
            }
        }
    }
}