using System.Text.RegularExpressions;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Docs;

public class XRucAttribute : XRegexAttribute
{
    private const string Message = "O campo {0} não possui um RUC válido.";

    private static readonly Regex Regex =
        new Regex(
            ValidationDataFormats.DocRucRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    public XRucAttribute() : base(Regex, Message)
    {
        Mode = EValidationMode.MaskRegEx;
        MaskForEditor = ValidationDataFormats.DocRucRegex;
    }

    public override bool Enabled => ValidationSettings.InParaguayMode;

    protected override bool InternalIsValid(object value)
        => XHelper.Strings.IsEmptyOrWhiteSpace(value?.ToString()) || XHelper.Data.Validations.IsRuc(value.ToString());
}