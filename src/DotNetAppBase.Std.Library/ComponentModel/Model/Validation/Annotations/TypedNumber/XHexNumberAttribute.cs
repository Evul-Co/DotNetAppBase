using System.Text.RegularExpressions;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Behaviors;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedNumber;

public class XHexNumberAttribute : XRegexAttribute, IMaxLengthConstraint
{
    private const string Message = "O campo {0} deve possui um valor válido.";

    private static readonly Regex Regex =
        new Regex(
            ValidationDataFormats.HexNumberRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    private readonly int _maxLenght;

    public XHexNumberAttribute(int maxLenght) : base(Regex, Message)
    {
        _maxLenght = maxLenght;

        Mode = EValidationMode.Custom;
    }

    int IMaxLengthConstraint.Value => _maxLenght;
}