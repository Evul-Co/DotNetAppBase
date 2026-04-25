using System;
using System.Text;

namespace DotNetAppBase.Std.Exceptions.Manager;

public class FormatEmailExceptionEventArgs : EventArgs
{
    public FormatEmailExceptionEventArgs(Exception exception)
    {
        Exception = exception;

        Output = new StringBuilder();
    }

    public Exception Exception { get; }

    public StringBuilder Output { get; }
}