using System.Data.Common;
using DotNetAppBase.Std.Db.Enums;

namespace DotNetAppBase.Std.Db.Contract;

public interface IDbDatabase : IDatabase
{
    int CommandTimeout { get; set; }

    string ConnectionString { get; }

    int ConnectionTimeout { get; set; }

    IDbSession DefaultSession { get; }

    string Name { get; }

    EDatabaseOption Options { get; }

    DbAccess GetAccess();

    DbConnection NewConnection();

    IDbSession NewSession();
}