using System.Text.RegularExpressions;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Behaviors;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString;

public class XPasswordAttribute : XRegexAttribute, IMaxLengthConstraint
{
    private const string Message = "O campo {0} deve possui de 4 a 20 caracteres.";

    private static readonly Regex Regex =
        new Regex(
            ValidationDataFormats.PasswordRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    public XPasswordAttribute() : base(Regex, Message, EDataType.Password)
    {
        Mode = EValidationMode.Custom;
    }

    int IMaxLengthConstraint.Value => 20;
}