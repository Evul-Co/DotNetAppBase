namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedNumber;

public class XYearAttribute : XRangeAttribute
{
    public XYearAttribute() : base(XHelper.Models.LessDbDateTimeValida.Year, 2100) { }
}