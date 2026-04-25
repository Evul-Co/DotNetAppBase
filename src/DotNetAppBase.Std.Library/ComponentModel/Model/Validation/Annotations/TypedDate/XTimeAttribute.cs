using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Behaviors;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Enums;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedDate;

public class XTimeAttribute : XValidationAttribute, IDateTimeConstraint
{
    public XTimeAttribute(bool useAdvancingCaret = true)
        : base(EDataType.Date, useAdvancingCaret ? EValidationMode.MaskDateTimeAdvancingCaret : EValidationMode.MaskDataTime) { }

    public override string Mask => "t";

    public EDateTimeFormat Format => EDateTimeFormat.Time;
    protected override bool InternalIsValid(object value) => true;
}