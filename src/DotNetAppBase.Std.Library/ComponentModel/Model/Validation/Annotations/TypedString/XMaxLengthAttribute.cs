using System;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Behaviors;
using DotNetAppBase.Std.Library.Properties;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedString;

public class XMaxLengthAttribute : XValidationAttribute, IMaxLengthConstraint
{
    public XMaxLengthAttribute(int value) : this(EDataType.Custom, value) { }

    public XMaxLengthAttribute(EDataType dataType, int value)
        : base(dataType, EValidationMode.Custom, string.Format(DbMessages.XMaxLengthAttribute_Message, "{0}", value))
    {
        Value = value;
    }

    public override string Mask => null;

    public int Value { get; }

    protected override bool InternalIsValid(object value)
    {
        if (value == null)
        {
            return true;
        }

        var input = value.As<string>();

        if (input.IsEmptyOrWhiteSpace())
        {
            return true;
        }

        return input.Length <= Value;
    }
}