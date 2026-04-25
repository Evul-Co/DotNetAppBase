using System;
using System.Linq;
using System.Threading.Tasks;
using DotNetAppBase.Std.Library.ComponentModel.Model.Svc.Enums;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Svc;

/// <summary>
/// Estrutura que representa um resultado
/// </summary>
/// <typeparam name="TData"></typeparam>
public class TypedResult<TData>
{
    /// <summary>
    /// Dado produzido e associado ao resultado
    /// </summary>
    public TData Data { get; set; }

    /// <summary>
    /// Detalhes a respeito do resultado
    /// </summary>
    public ResultInfo[] Details { get; set; }

    /// <summary>
    /// Mensagem associada ao resultado
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Resultado está Ok?
    /// </summary>
    public bool Ok => Status == EResultStatus.Ok;

    /// <summary>
    /// Status associado ao resultado
    /// </summary>
    public EResultStatus Status { get; set; }

    public static TypedResult<TData> Clone<TInput>(TypedResult<TInput> origin, TData data = default) =>
        new TypedResult<TData>
            {
                Message = origin.Message,
                Status = origin.Status,
                Details = origin.Details,
                Data = data
            };

    public static TypedResult<TData> Error(string error) =>
        new TypedResult<TData>
            {
                Message = error,
                Status = EResultStatus.Error,
            };

    public static TypedResult<TData> Error(ServiceResponse response)
    {
        return new TypedResult<TData>
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

    public static TypedResult<TData> Exception(Exception ex) => Error(ex.Message);

    public static TypedResult<TData> Success(TData data, string success = null) =>
        new TypedResult<TData>
            {
                Data = data,
                Message = success,
                Status = EResultStatus.Ok,
            };

    public static async Task<TypedResult<TData>> Success(Func<Task<TData>> funcTask)
    {
        var data = await funcTask();

        return Success(data);
    }

    public static TypedResult<TData> Success(string success) =>
        new TypedResult<TData>
            {
                Message = success,
                Status = EResultStatus.Ok,
            };

    public static TypedResult<TData> Warning(string alert) =>
        new TypedResult<TData>
            {
                Message = alert,
                Status = EResultStatus.Warning,
            };
}