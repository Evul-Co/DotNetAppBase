namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedBinary;

public class XAvatarAttribute : XImageAttribute
{
    public const long DefaultNormalSize = XHelper.UnitOfMeasure.Megabyte * 2;

    public XAvatarAttribute() : base(DefaultNormalSize) { }
}