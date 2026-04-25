using System.Text.RegularExpressions;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Geo;

public class XUnidadeFederativaAttribute : XRegexAttribute
{
    private const string Message = "O campo {0} não possui um valor válido.";

    private static readonly Regex Regex =
        new Regex(
            ValidationDataFormats.GeoEstadoIDRegex,
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    public XUnidadeFederativaAttribute() : base(Regex, Message) { }
}