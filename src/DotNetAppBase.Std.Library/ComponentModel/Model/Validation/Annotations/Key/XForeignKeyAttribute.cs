using System;
using System.ComponentModel.DataAnnotations;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Behaviors;
using DotNetAppBase.Std.Library.Properties;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.Key;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class XForeignKeyAttribute : ValidationAttribute
{
    public XForeignKeyAttribute()
    {
        ErrorMessage = DbMessages.XForeignKeyAttribute_XForeignKeyAttribute_O_campo__0__deve_ser_informado_;
    }

    public string NavigationPropertyName { get; set; }

    public bool UpdatedProgrammatically { get; set; }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (UpdatedProgrammatically || validationContext == null)
        {
            return ValidationResult.Success;
        }

        var propertyInfo = XHelper.Reflections.Properties.Get(validationContext.ObjectType, validationContext.MemberName);

        var isNullable = XHelper.Types.IsNullable(propertyInfo.PropertyType);
        if (isNullable && value == null)
        {
            return ValidationResult.Success;
        }

        if (!int.TryParse(value.ToString(), out var fkValue) || !XHelper.Models.FkIsNull(fkValue))
        {
            return ValidationResult.Success;
        }

        if (isNullable)
        {
            XHelper.Reflections.Properties.WriteValue(validationContext.ObjectInstance, propertyInfo, null);
        }
        else if (!TryIsNotNullValidNavigationProperty(validationContext))
        {
            return GetErrorResult(validationContext);
        }

        return ValidationResult.Success;
    }

    private ValidationResult GetErrorResult(ValidationContext validationContext) => new ValidationResult(string.Format(ErrorMessage, validationContext?.DisplayName));

    private bool TryIsNotNullValidNavigationProperty(ValidationContext validationContext)
    {
        if (!BehaviorIdentifier.Fk.TryGetNavigationProperty(
                validationContext.ObjectType, NavigationPropertyName ?? validationContext.MemberName, out var navPropertyInfo))
        {
            return false;
        }

        return navPropertyInfo.GetValue(validationContext.ObjectInstance) != null;
    }
}