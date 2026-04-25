using System.Data.Common;

namespace DotNetAppBase.Std.Db.Contract;

public interface IDbTransactionManager
{
    DbConnection Connection { get; }

    bool InTransaction { get; }

    IDbSession Session { get; }

    DbTransaction Transaction { get; }

    void Commit();

    void Rollback();

    void StartTransaction();
}