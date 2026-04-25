using System;

// ReSharper disable CheckNamespace
public static class XEventExtensions
// ReSharper restore CheckNamespace
{
    public static void Raise(this EventHandler eventHandler, object sender)
    {
        eventHandler?.Invoke(sender, EventArgs.Empty);
    }

    public static void Raise<TEventArgs>(this EventHandler<TEventArgs> eventHandler,
        object sender, Func<TEventArgs> funcGetArgs)
        where TEventArgs : EventArgs
    {
        eventHandler?.Invoke(sender, funcGetArgs());
    }
}