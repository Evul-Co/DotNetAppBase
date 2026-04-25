using System.Data.Common;

namespace DotNetAppBase.Std.Db.Contract;

public interface IDbSession : IDbAccessProvider
{
    IDbDatabase Database { get; }

    bool InTransaction { get; }

    IDbTransactionManager TransactionManager { get; }

    void BeginTransaction();

    IDbContext BuildContext();

    void Commit();

    DbDataAdapter CreateDataAtapter(DbCommand cmd);

    DbParameter CreateReturnParameter();

    bool RetryInteractionOnDbExcepion(DbException exception);

    void Rollback();
}