using System;

namespace DotNetAppBase.Std.Library.Concurrency;

public class XGet<T> : XLazy<T>
{
    public XGet(Func<T> valueFactory, Action<T> releaseAction = null, Action<T> onCreated = null) : base(valueFactory, releaseAction, onCreated) { }

    public new T Value
    {
        get => base.Value;
        set => base.Value = value;
    }
}