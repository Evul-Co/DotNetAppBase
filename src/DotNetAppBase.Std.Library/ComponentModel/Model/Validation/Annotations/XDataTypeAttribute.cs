using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations;

public abstract class XDataTypeAttribute : DataTypeAttribute
{
    protected XDataTypeAttribute(EDataType dataType, bool isComputed = false) : base(Translate(dataType))
    {
        DataType = dataType;

        IsComputed = isComputed;
    }

    public new EDataType DataType { get; }

    public bool IsComputed { get; }

    public override string GetDataTypeName()
    {
        if (base.DataType == System.ComponentModel.DataAnnotations.DataType.Custom)
        {
            return "Custom";
        }

        return base.GetDataTypeName();
    }

    public sealed override bool IsValid(object value) => IsComputed || !InternalIsEnabled() || InternalIsValid(value);

    protected virtual bool InternalIsEnabled() => true;

    protected virtual bool InternalIsValid(object value) => base.IsValid(value);

    protected virtual ValidationResult InternalIsValid(object value, ValidationContext validationContext) => base.IsValid(value, validationContext);

    protected sealed override ValidationResult IsValid(object value, ValidationContext validationContext) => IsComputed || !InternalIsEnabled() ? ValidationResult.Success : InternalIsValid(value, validationContext);

    private static DataType Translate(EDataType dataType)
    {
        var translated = XHelper.Enums.TryGetValue<DataType>(dataType.ToString());

        return translated ?? System.ComponentModel.DataAnnotations.DataType.Custom;
    }
}