using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Behaviors;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Enums;
using DotNetAppBase.Std.Library.Properties;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedDate;

public class XDateTimeAttribute : XValidationAttribute, IDateTimeConstraint
{
    public XDateTimeAttribute(bool useAdvancingCaret = true, bool isComputed = false)
        : base(
            EDataType.DateTime,
            useAdvancingCaret ? EValidationMode.MaskDateTimeAdvancingCaret : EValidationMode.MaskDataTime,
            isComputed)
    {
        ErrorMessage = DbMessages.XDateTimeAttribute_XDateTimeAttribute_O_campo__0__deve_ser_informado;
    }

    // ReSharper disable LocalizableElement
    public override string Mask => "G";
    // ReSharper restore LocalizableElement

    public EDateTimeFormat Format => EDateTimeFormat.DateTime;

    protected override bool InternalIsValid(object value) => true;

    protected override ValidationResult InternalIsValid(object value, ValidationContext validationContext)
    {
        if (validationContext == null)
        {
            return ValidationResult.Success;
        }

        var propertyInfo = XHelper.Reflections.Properties.Get(validationContext.ObjectType, validationContext.MemberName);

        if (XHelper.Types.IsNullable(propertyInfo.PropertyType) && value == null)
        {
            return ValidationResult.Success;
        }

        if (DateTime.TryParse(value.ToString(), out var dateTime) && !XHelper.Models.DateTimeIsValid(dateTime))
        {
            return GetErrorResult(validationContext);
        }

        return ValidationResult.Success;
    }

    private ValidationResult GetErrorResult(ValidationContext validationContext) => new ValidationResult(string.Format(ErrorMessage, validationContext?.DisplayName));
}