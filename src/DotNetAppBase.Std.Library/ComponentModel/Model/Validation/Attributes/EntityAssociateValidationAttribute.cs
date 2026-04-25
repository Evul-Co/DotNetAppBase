using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Validation.Attributes;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property)]
public class EntityAssociateValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is IEnumerable enumerable)
        {
            var violations = new List<ValidationResult>();
            enumerable.Cast<object>()
                .ToArray()
                .ForEach(
                    model =>
                        {
                            var validation = EntityValidator.Validate(model);

                            if (!validation.HasViolations)
                            {
                                return;
                            }

                            violations.AddRange(validation.Validations);
                        });

            if (violations.Count == 0)
            {
                return ValidationResult.Success;
            }

            return new EntityAssociateValidationResult(
                $"Existem dados incorretos associados a '{validationContext.DisplayName}'",
                violations);
        }

        var validationResult = EntityValidator.Validate(value);

        if (!validationResult.HasViolations)
        {
            return ValidationResult.Success;
        }

        return new EntityAssociateValidationResult(
            $"Existens dados incorretos associados a '{validationContext.DisplayName}'",
            validationResult.Validations);
    }
}