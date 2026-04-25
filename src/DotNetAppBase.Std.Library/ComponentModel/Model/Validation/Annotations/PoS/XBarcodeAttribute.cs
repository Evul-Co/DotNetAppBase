using System.Text.RegularExpressions;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.PoS;

public class XBarcodeAttribute : XRegexAttribute
{
    private const string Message = "O campo {0} não possui um valor válido.";

    private static readonly Regex Regex =
        new Regex(
            ValidationDataFormats.KeyBarcodeRegex,
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    public XBarcodeAttribute() : base(Regex, Message)
    {
        Mode = EValidationMode.MaskSimple;
        MaskForEditor = ValidationDataFormats.KeyBarcodeMask;
    }
}