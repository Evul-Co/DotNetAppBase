using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Geo;

public class XLogradouroEnderecoAttribute : XMaxLengthAttribute
{
    public XLogradouroEnderecoAttribute() : base(150) { }
}