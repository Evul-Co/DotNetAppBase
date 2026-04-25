using System;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString;

public class XXmlAttribute : XMaxLengthAttribute
{
    public XXmlAttribute() : base(EDataType.Xml, int.MaxValue) { }
}