using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString;

public class XMinLengthAttribute : MinLengthAttribute
{
    private const string Message = "O campo {0} não pode ser menor que {1} caracter(es).";

    public XMinLengthAttribute(int length) : base(length) { }

    public override string FormatErrorMessage(string name) => string.Format(Message, name, Length);
}