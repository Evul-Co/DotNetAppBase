using System.ComponentModel;
using DotNetAppBase.Std.Exceptions.Base;

namespace DotNetAppBase.Std.Exceptions.Assert;

public class XContractFailException : XException
{
    protected XContractFailException(string message) : base(message) { }

    [Localizable(false)]
    public static XContractFailException Create(string message) => new XContractFailException(message);
}