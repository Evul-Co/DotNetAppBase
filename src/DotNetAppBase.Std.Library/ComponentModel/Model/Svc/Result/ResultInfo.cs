namespace DotNetAppBase.Std.Library.ComponentModel.Model.Svc;

/// <summary>
/// Detalhe associado a um resultado
/// </summary>
public class ResultInfo
{
    /// <summary>
    /// Chave que orienta a interpretação da mensagem
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Mensagem
    /// </summary>
    public string Message { get; set; }
}