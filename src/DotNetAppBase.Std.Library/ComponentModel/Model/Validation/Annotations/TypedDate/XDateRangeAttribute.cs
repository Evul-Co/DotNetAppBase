using System;
using System.ComponentModel.DataAnnotations;
using DotNetAppBase.Std.Library.ComponentModel.Model.Abstraction;
using DotNetAppBase.Std.Library.Properties;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Annotations.TypedDate;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class XDateRangeAttribute : XValidationAttribute
{
    public XDateRangeAttribute() : base(EDataType.Custom, EValidationMode.Custom) { }

    public bool AllowIncompleteRange { get; set; }

    public override string Mask => null;

    public int MaxRange { get; set; }

    public int MinRange { get; set; }

    protected override ValidationResult InternalIsValid(object value, ValidationContext validationContext)
    {
        var dateRange = value.As<IDateRange>();

        if (dateRange == null)
        {
            return ValidationResult.Success;
        }

        if (!dateRange.IsNull)
        {
            return ValidationResult.Success;
        }

        if (dateRange.IsNullPartial)
        {
            return AllowIncompleteRange
                ? ValidationResult.Success
                : GetErrorResult(DbMessages.XDateRangeAttribute_IsValid_O_período_informado_está_incompleto_, validationContext);
        }

        var range = dateRange.Range;

        if (range.TotalDays < 0)
        {
            return GetErrorResult(
                DbMessages.XDateRangeAttribute_IsValid_Período_informado_é_inválido_, validationContext);
        }

        if (MinRange != 0)
        {
            if (range.TotalDays < MinRange)
            {
                return GetErrorResult(
                    string.Format(DbMessages.XDateRangeAttribute_IsValid_O_período_mínimo_deve_ser_de__0__dia_s__, MinRange), validationContext);
            }
        }

        if (MaxRange != 0)
        {
            if (range.TotalDays > MaxRange)
            {
                return GetErrorResult(
                    string.Format(DbMessages.XDateRangeAttribute_IsValid_O_período_máximo_deve_ser_de__0__dia_s__, MinRange), validationContext);
            }
        }

        return ValidationResult.Success;
    }

    protected override bool InternalIsValid(object value) => true;

    private static ValidationResult GetErrorResult(string errorMessage, ValidationContext validationContext)
    {
        return new ValidationResult(errorMessage, new[] {validationContext.MemberName});
    }
}