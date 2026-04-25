using System.Text.RegularExpressions;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Docs;

public class XCpfAttribute : XRegexAttribute
{
    private const string Message = "O campo {0} não possui um CPF válido.";

    private static readonly Regex Regex =
        new Regex(
            ValidationDataFormats.DocCpfRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    public XCpfAttribute() : base(Regex, Message)
    {
        Mode = EValidationMode.MaskSimple;
        MaskForEditor = ValidationDataFormats.DocCpfMask;
    }

    public override bool Enabled => !ValidationSettings.InParaguayMode;

    protected override bool InternalIsValid(object value)
        => XHelper.Strings.IsEmptyOrWhiteSpace(value?.ToString()) || XHelper.Data.Validations.IsCpf(value.ToString());
}