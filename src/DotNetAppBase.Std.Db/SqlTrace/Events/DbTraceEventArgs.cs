using System;
using System.Data.Common;

namespace DotNetAppBase.Std.Db.SqlTrace;

public class DbTraceEventArgs : EventArgs
{
    public DbTraceEventArgs(DbCommand command, string connectionString)
    {
        Command = command;
        ConnectionString = connectionString;
    }

    public DbCommand Command { get; }

    public string ConnectionString { get; }
}