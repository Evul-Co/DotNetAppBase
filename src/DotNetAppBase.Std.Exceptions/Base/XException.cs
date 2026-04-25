using System;
using System.ComponentModel;

namespace DotNetAppBase.Std.Exceptions.Base;

[Serializable, Localizable(false)]
public class XException : ApplicationException
{
    public XException() { }

    public XException([Localizable(false)] string message) : base(message) { }

    public XException(string message, Exception innerException) : base(message, innerException) { }

    public static bool IsOne(Exception ex, out XException xEx)
    {
        if (ex is XException exception)
        {
            xEx = exception;
            return true;
        }

        if (XDbException.IsOne(ex, out var dbException))
        {
            xEx = dbException;
            return true;
        }

        xEx = null;
        return false;
    }
}