using System;
using System.ComponentModel.DataAnnotations;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Behaviors;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Enums;
using DotNetAppBase.Std.Library.Properties;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedDate;

public class XDateAttribute : XValidationAttribute, IDateTimeConstraint
{
    public XDateAttribute(bool useAdvancingCaret = true)
        : base(EDataType.Date, useAdvancingCaret ? EValidationMode.MaskDateTimeAdvancingCaret : EValidationMode.MaskDataTime)
    {
        ErrorMessage = DbMessages.XDateTimeAttribute_XDateTimeAttribute_O_campo__0__deve_ser_informado;
    }

    // ReSharper disable LocalizableElement
    public override string Mask => "d";
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