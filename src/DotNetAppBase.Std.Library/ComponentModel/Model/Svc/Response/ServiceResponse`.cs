using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Svc;

[DataContract, Serializable]
public class ServiceResponse<TEntity> : ServiceResponse
{
    public ServiceResponse(TEntity entity, EntityValidationResult validationResult = null)
    {
        Entity = entity;

        ValidationResult = validationResult ?? new EntityValidationResult();
    }

    public ServiceResponse(TEntity entity, params string[] errors)
    {
        Entity = entity;

        ValidationResult = new EntityValidationResult(errors.Select(error => new ValidationResult(error)));
    }

    [DataMember]
    public TEntity Entity { get; }

    public override string ToString()
    {
        var bd = new StringBuilder();

        // ReSharper disable LocalizableElement
        ValidationResult.Validations.ForEach(result => bd.Append($"- {result.ErrorMessage} \n"));
        // ReSharper restore LocalizableElement

        return bd.ToString();
    }
}