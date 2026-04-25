using System;

// ReSharper disable CheckNamespace
public static class XLazyExtensions
// ReSharper restore CheckNamespace
{
    public static Lazy<TReturn> AsLazy<TReturn>(this Func<TReturn> func)
    {
        if (func == null)
        {
            throw new ArgumentNullException(nameof(func));
        }

        return new Lazy<TReturn>(func);
    }

    public static Lazy<TReturn> AsLazy<TParam, TReturn>(this Func<TParam, TReturn> func, TParam value)
    {
        if (func == null)
        {
            throw new ArgumentNullException(nameof(func));
        }

        return new Lazy<TReturn>(() => func(value));
    }

    public static TReturn ValueIfCreated<TReturn>(this Lazy<TReturn> lazy) where TReturn : class => lazy.IsValueCreated ? lazy.Value : null;
}