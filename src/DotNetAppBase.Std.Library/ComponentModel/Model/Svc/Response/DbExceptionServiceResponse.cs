using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Runtime.Serialization;
using DotNetAppBase.Std.Library.ComponentModel.Model.Validation;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Svc;

[DataContract, Serializable]
public class DbExceptionServiceResponse<T> : ServiceResponse<T> where T : class
{
    // ReSharper disable SuggestBaseTypeForParameter
    public DbExceptionServiceResponse(DbException exception) : this(null, exception) { }
    // ReSharper restore SuggestBaseTypeForParameter

    public DbExceptionServiceResponse(T entity, Exception exception) : base(entity, CreateResult(exception)) { }

    private static EntityValidationResult CreateResult(Exception exception) => new EntityValidationResult(new[] {new ValidationResult(exception.Message)});
}