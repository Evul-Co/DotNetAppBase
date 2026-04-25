namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedNumber;

public class XMonthAttribute : XRangeAttribute
{
    public XMonthAttribute() : base(1, 12) { }
}