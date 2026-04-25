using System;
using System.Data.Common;
using DotNetAppBase.Std.Db.Enums;

namespace DotNetAppBase.Std.Db.Contract;

public interface IDbContext : IDisposable
{
    DbConnection Connection { get; }

    bool InTransaction { get; }

    bool IsAvailable { get; }

    EDbContextState State { get; }

    DbTransaction Transaction { get; }

    void Close();

    DbCommand CreateCommand();

    void Open();
}