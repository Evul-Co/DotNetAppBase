namespace DotNetAppBase.Std.Db.Enums;

/// <summary>
///     Determina o estado de um ISqlContext
/// </summary>
public enum EDbContextState
{
    /// <summary>
    ///     Objeto de acesso não está em um contexto de transação
    /// </summary>
    OutTransaction,

    /// <summary>
    ///     Objeto de acesso está em um contexto de transação
    /// </summary>
    InTransaction,

    /// <summary>
    ///     Transação foi confirmada (Commit)
    /// </summary>
    Confirmed,

    /// <summary>
    ///     Transação foi cancelada (Rollback)
    /// </summary>
    Cancelled,

    /// <summary>
    ///     Quando o contexto liberou os recursos utilizados, tornando-se indisponível
    /// </summary>
    Disposed
}