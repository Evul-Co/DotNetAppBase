using System;
using DotNetAppBase.Std.Library;

// ReSharper disable CheckNamespace
public static class XDelegateExtensions
// ReSharper restore CheckNamespace
{
    public static void SetTimeOut(this Delegate method, TimeSpan wait, params object[] args)
    {
        XHelper.Async.SetTimeOut(method, wait, args);
    }
}