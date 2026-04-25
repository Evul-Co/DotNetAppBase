using System.Text.RegularExpressions;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Geo;

public class XLogradouroCepAttribute : XRegexAttribute
{
    private const string Message = "O campo {0} não possui um CEP válido.";

    private static readonly Regex Regex =
        new Regex(
            ValidationDataFormats.GeoCepRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    public XLogradouroCepAttribute() : base(Regex, Message)
    {
        Mode = EValidationMode.MaskRegular;
        MaskForEditor = ValidationDataFormats.GeoCepMask;
    }
}