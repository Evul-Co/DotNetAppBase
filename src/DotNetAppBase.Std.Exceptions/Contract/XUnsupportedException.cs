using System;
using System.ComponentModel;
using DotNetAppBase.Std.Exceptions.Base;

namespace DotNetAppBase.Std.Exceptions.Contract;

public class XUnsupportedException : XException
{
    protected XUnsupportedException([Localizable(false)] string message) : base(message) { }

    public static XUnsupportedException Create(string message) => new XUnsupportedException(message);

    public static XUnsupportedException TypeDoesNotSupport(Type type, string operationDetail)
        => new XUnsupportedException($"The type {type.Name} does not support operation: {operationDetail}");
}