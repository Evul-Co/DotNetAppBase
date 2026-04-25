using System.Text.RegularExpressions;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Docs;

public class XInscricaoMunicipalAttribute : XRegexAttribute
{
    private const string Message = "O campo {0} não possui uma Inscrição Municipal válida.";

    private static readonly Regex Regex =
        new Regex(
            ValidationDataFormats.DocInscMunicipalRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    public XInscricaoMunicipalAttribute() : base(Regex, Message)
    {
        Mode = EValidationMode.MaskSimple;
        MaskForEditor = ValidationDataFormats.DocInscMunicipalMask;
    }
}