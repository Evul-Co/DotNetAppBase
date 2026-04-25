using System.Text.RegularExpressions;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Docs;

public class XCnpjAttribute : XRegexAttribute
{
    private const string Message = "O campo {0} não possui um CNPJ válido.";

    private static readonly Regex Regex =
        new Regex(
            ValidationDataFormats.DocCnpjRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    public XCnpjAttribute() : base(Regex, Message)
    {
        Mode = EValidationMode.MaskSimple;
        MaskForEditor = ValidationDataFormats.DocCnpjMask;
    }

    public override bool Enabled => !ValidationSettings.InParaguayMode;

    protected override bool InternalIsValid(object value)
        => XHelper.Strings.IsEmptyOrWhiteSpace(value?.ToString()) || XHelper.Data.Validations.IsCnpj(value.ToString());
}