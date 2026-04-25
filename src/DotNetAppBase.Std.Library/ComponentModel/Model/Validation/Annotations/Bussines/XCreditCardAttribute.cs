using System;
using System.ComponentModel;
using System.Linq;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Bussines;

[Localizable(false)]
public sealed class XCreditCardAttribute : XDataTypeAttribute
{
    private const string Message = "O campo {0} não possui um número de cartão de crédito válido.";

    public XCreditCardAttribute() : base(EDataType.Custom)
    {
        ErrorMessage = Message;
    }

    protected override bool InternalIsValid(object value)
    {
        if (value == null)
        {
            return true;
        }

        if (!(value is string str1))
        {
            return false;
        }

        var str2 = str1.Replace("-", string.Empty).Replace(" ", string.Empty);
        var num1 = 0;
        var flag = false;

        foreach (var ch in str2.Reverse())
        {
            if (ch < 48 || ch > 57)
            {
                return false;
            }

            var num2 = (ch - 48) * (flag ? 2 : 1);
            flag = !flag;
            while (num2 > 0)
            {
                num1 += num2 % 10;
                num2 /= 10;
            }
        }

        return num1 % 10 == 0;
    }
}