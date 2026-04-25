using System;
using DotNetAppBase.Std.Exceptions.Base;

namespace DotNetAppBase.Std.Exceptions.Bussines;

public abstract class XBussinesException : XException
{
    protected XBussinesException() { }

    protected XBussinesException(string message) : base(message) { }

    protected XBussinesException(string message, Exception innerException) : base(message, innerException) { }
}