using System;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedNumber;

public class XNumberAttribute : XValidationAttribute
{
    public XNumberAttribute()
        : this(EDataType.Custom, EValidationMode.MaskRegEx) { }

    protected XNumberAttribute(EDataType dataType, EValidationMode mode)
        : base(dataType, mode) { }

    public override string Mask => throw new NotImplementedException();

    public int Precision { get; set; }

    protected override bool InternalIsValid(object value) => true;
}