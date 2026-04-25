using System;

// ReSharper disable UnusedMember.Global

namespace DotNetAppBase.Std.Library.Concurrency;

public class XLazy<T>
{
    private readonly Action<T> _onCreated;
    private readonly Action<T> _releaseAction;
    private readonly object _sync = new object();

    private readonly Func<T> _valueFactory;

    private T _value;

    public XLazy(Func<T> valueFactory, Action<T> releaseAction = null, Action<T> onCreated = null)
    {
        _valueFactory = valueFactory;
        _releaseAction = releaseAction;
        _onCreated = onCreated;
    }

    public bool IsValueCreated { get; private set; }

    public virtual T Value
    {
        get
        {
            lock (_sync)
            {
                CheckToInvalidate();

                if (!IsValueCreated)
                {
                    IsValueCreated = true;
                    _value = _valueFactory();

                    _onCreated?.Invoke(_value);
                }

                return _value;
            }
        }

        protected set
        {
            lock (_sync)
            {
                if (!IsValueCreated)
                {
                    IsValueCreated = true;
                    _value = value;

                    _onCreated?.Invoke(_value);
                }
            }
        }
    }

    public T ValueIfCreated
    {
        get
        {
            lock (_sync)
            {
                return _value;
            }
        }
    }

    public void Execute()
    {
        // ReSharper disable UnusedVariable
        var obj = Value;
        // ReSharper restore UnusedVariable
    }

    public void ReCreate()
    {
        Reset();

        Execute();
    }

    public void Reset()
    {
        T lValue;
        lock (_sync)
        {
            if (!IsValueCreated)
            {
                return;
            }

            lValue = _value;

            IsValueCreated = false;
            _value = default;
        }

        _releaseAction?.Invoke(lValue);
    }

    protected virtual void CheckToInvalidate() { }
}