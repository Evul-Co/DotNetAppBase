using System.Text.RegularExpressions;
using DotNetAppBase.Std.Library.Properties;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Docs;

public class XRucOrCiAttribute : XRegexAttribute
{
    private static readonly Regex Regex =
        new Regex(
            ValidationDataFormats.DocRucRegex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    public XRucOrCiAttribute() : base(Regex, DbMessages.XRucOrCiAttribute_XRucOrCiAttribute_O_campo__0__não_possui_um_RUC_válido_)
    {
        Mode = EValidationMode.MaskRegEx;
        MaskForEditor = ValidationDataFormats.DocRucRegex;
    }

    public override bool Enabled => ValidationSettings.InParaguayMode;

    protected override bool InternalIsValid(object value)
    {
        if (XHelper.Strings.IsEmptyOrWhiteSpace(value?.ToString()))
        {
            return false;
        }

        var documentoID = value.ToString();

        // ReSharper disable LocalizableElement
        var isRuc = documentoID.Contains("-");
        // ReSharper restore LocalizableElement

        return !isRuc || XHelper.Data.Validations.IsRuc(value.ToString());
    }
}