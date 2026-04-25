using System.Text.RegularExpressions;
using DotNetAppBase.Std.Library.ComponentModel.Model.Enums;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Geo;

public class XCodigoIbgeAttribute : XRegexAttribute
{
    private const string Message = "O campo {0} não possui um valor válido.";

    private static readonly Regex Regex =
        new Regex(
            ValidationDataFormats.GeoMunicipioIDRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    public XCodigoIbgeAttribute() : base(Regex, Message)
    {
        Mode = EValidationMode.MaskRegular;
        MaskForEditor = ValidationDataFormats.GeoMunicipioIDMask;

        RestrictFor = EModoOperacao.Brasil;
    }
}