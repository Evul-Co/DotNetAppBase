namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString;

public class XNameLargeAttribute : XMaxLengthAttribute
{
    public XNameLargeAttribute() : base(120) { }
}