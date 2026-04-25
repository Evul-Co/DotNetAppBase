using System;
using System.Linq;
using System.Threading.Tasks;
using DotNetAppBase.Std.Library.ComponentModel.Model.Svc.Enums;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Svc;

public class Result : TypedResult<object>
{
    public new static Result Error(string error) =>
        new Result
            {
                Message = error,
                Status = EResultStatus.Error,
            };

    public new static Result Error(ServiceResponse response)
    {
        return new Result
            {
                Message = "Ocorreu um erro no processo, verifique o(s) detalhe(s),",
                Status = response.Status == EServiceResponse.Succeeded ? EResultStatus.Ok : EResultStatus.Error,
                Details = response.ValidationResult.Validations.Select(
                    validationResult =>
                        {
                            var key = validationResult.MemberNames.Any()
                                ? validationResult.MemberNames.Aggregate((s, s1) => s + ";" + s1)
                                : string.Empty;

                            return new ResultInfo
                                {
                                    Key = key,
                                    Message = validationResult.ErrorMessage
                                };
                        }).ToArray()
            };
    }

    public new static Result Exception(Exception ex) => Error(ex.Message);

    public static Result Success(object data) => new Result
        {
            Data = data,
            Status = EResultStatus.Ok,
        };

    public new static async Task<Result> Success(Func<Task<object>> funcTask)
    {
        var data = await funcTask();

        return Success(data);
    }

    public new static Result Success(string success) =>
        new Result
            {
                Message = success,
                Status = EResultStatus.Ok,
            };

    public new static Result Warning(string alert) =>
        new Result
            {
                Message = alert,
                Status = EResultStatus.Warning,
            };
}