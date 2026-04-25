using System.Text;
using DotNetAppBase.Std.Exceptions.Base;
using DotNetAppBase.Std.Exceptions.Bussines;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Svc;

public static class ServiceResponseExtensions
{
    public static void Join<TEntity>(this ServiceResponse<TEntity> @this, ServiceResponse<TEntity> response) => @this.ValidationResult.Join(response.ValidationResult);

    public static void Join<TEntity>(this ServiceResponse<TEntity> @this, EntityValidationResult validationResult) => @this.ValidationResult.Join(validationResult);

    public static XException TranslateToException<TEntity>(this ServiceResponse<TEntity> @this, string message)
    {
        if (@this.Success)
        {
            return null;
        }

        var builder = new StringBuilder(message);

        foreach (var validation in @this.ValidationResult.Validations)
        {
            // ReSharper disable LocalizableElement
            builder.Append($"\n\t- {validation.ErrorMessage}");
            // ReSharper restore LocalizableElement
        }

        return XFlowException.Create(builder.ToString());
    }
}