using System;
using System.ComponentModel;

namespace DotNetAppBase.Std.Exceptions.Contract;

[Serializable]
public class XArgumentOutOfRangeException : ArgumentOutOfRangeException
{
    protected XArgumentOutOfRangeException(string paramName) : base(paramName) { }

    protected XArgumentOutOfRangeException(string paramName, string message, object actualValue) : base(paramName, actualValue, message) { }

    public static XArgumentOutOfRangeException Create(string paramName) => new XArgumentOutOfRangeException(paramName);

    public static XArgumentOutOfRangeException Create(string paramName, [Localizable(false)] string message) => new XArgumentOutOfRangeException(paramName, message, null);

    public static XArgumentOutOfRangeException Create(string paramName, object actualValue, [Localizable(false)] string message) => new XArgumentOutOfRangeException(paramName, message, actualValue);
}