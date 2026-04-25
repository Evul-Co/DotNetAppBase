using System.Text.RegularExpressions;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations;

public class XRegexAttribute : XValidationAttribute
{
    private const string Message = "O campo {0} não possui um valor válido.";

    protected const RegexOptions DefaultRegexOptions = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled;

    private readonly Regex _regex;

    public XRegexAttribute(string regex, string errorMessage = null, EDataType dataType = EDataType.Custom)
        : this(new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled), errorMessage ?? Message, dataType) { }

    public XRegexAttribute(Regex regex, string errorMessage = null, EDataType dataType = EDataType.Custom) : base(dataType, EValidationMode.MaskRegEx, errorMessage)
    {
        _regex = regex;
    }

    public virtual bool DisconsiderEmptyValue => true;

    public override string Mask => _regex.ToString();

    protected override bool InternalIsValid(object value)
    {
        if (value == null)
        {
            return true;
        }

        var input = value.As<string>();

        if (DisconsiderEmptyValue && input.IsEmptyOrWhiteSpace())
        {
            return true;
        }

        if (input != null)
        {
            return _regex.Match(input).Length > 0;
        }

        return false;
    }
}