using System;
using System.ComponentModel;

namespace DotNetAppBase.Std.Exceptions.Bussines;

public class XFlowException : XBussinesException
{
    private const string DefaultMessage = "The flow fail with this reason: {0}.";

    protected XFlowException(string failReason) : base(string.Format(DefaultMessage, failReason)) { }

    public static XFlowException Create([Localizable(false)] string failReason) => new XFlowException(failReason);
}