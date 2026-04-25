using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.PoS;

public class XPrintNameAttribute : XMaxLengthAttribute
{
    public XPrintNameAttribute() : base(9) { }
}