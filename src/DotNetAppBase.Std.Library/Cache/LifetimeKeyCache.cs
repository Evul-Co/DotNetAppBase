using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetAppBase.Std.Library.Cache;

public class LifetimeKeyCache
{
    public class Item<TValue>
    {
        private long? _lifetime;

        public Item(TValue value, long? lifetime = null)
        {
            Value = value;
            _lifetime = lifetime;
        }

        public long Lifetime
        {
            get => _lifetime ?? 0;
            internal set => _lifetime = value;
        }

        public TValue Value { get; }

        internal DateTime Cached { get; set; }

        internal bool IsNullLifetime => _lifetime == null;
    }
}

public class LifetimeKeyCache<TKey, TValue> : LifetimeKeyCache
{
    private readonly Dictionary<TKey, Item<TValue>> _data;
    private readonly long? _defaultLifetime;
    private readonly Func<TKey, Item<TValue>> _funcCreate;
    private readonly object _sync = new object();

    public LifetimeKeyCache(Func<TKey, Item<TValue>> funcCreate, long? defaultLifetime = null)
    {
        _funcCreate = funcCreate;
        _defaultLifetime = defaultLifetime;

        _data = new Dictionary<TKey, Item<TValue>>();
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            lock (_sync)
            {
                return _data.Values.Select(item => item.Value);
            }
        }
    }

    public void Clear()
    {
        lock (_sync)
        {
            _data.Clear();
        }
    }

    public TValue Get(TKey key)
    {
        lock (_sync)
        {
            var now = DateTime.Now;

            if (_data.TryGetValue(key, out var item))
            {
                if (!item.IsNullLifetime && item.Cached.AddMilliseconds(item.Lifetime) < now)
                {
                    _data.Remove(key);
                }
                else
                {
                    return item.Value;
                }
            }

            item = _funcCreate(key);
            if (item == null)
            {
                return default;
            }

            item.Cached = now;

            if (item.IsNullLifetime && _defaultLifetime != null)
            {
                item.Lifetime = _defaultLifetime.Value;
            }

            _data.Add(key, item);

            return item.Value;
        }
    }
}