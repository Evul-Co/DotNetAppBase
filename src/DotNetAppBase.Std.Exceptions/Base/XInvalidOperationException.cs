using System;
using System.ComponentModel;

namespace DotNetAppBase.Std.Exceptions.Base;

public class XInvalidOperationException : XException
{
    public XInvalidOperationException() { }

    public XInvalidOperationException([Localizable(false)] string message) : base(message) { }

    public XInvalidOperationException(string message, Exception innerException) : base(message, innerException) { }
}