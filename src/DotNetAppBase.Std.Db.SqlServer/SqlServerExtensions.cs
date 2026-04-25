using System.ComponentModel;
using System.Data;
using DotNetAppBase.Std.Db.Contract;
#if NETFRAMEWORK
using System.Data.SqlClient;

#else
using Microsoft.Data.SqlClient;
#endif

namespace DotNetAppBase.Std.Db.SqlServer;

[Localizable(false)]
// ReSharper disable CheckNamespace
public static class SqlServerExtensions
// ReSharper restore CheckNamespace
{
    public static TResult ExecuteFunc<TResult>(this IDbAccess access, string funcName, params SqlParameter[] parameters) => access.ExecFunc<TResult, SqlParameter>(funcName, parameters);

    public static T ExecuteProc<T>(this IDbAccess access, [Localizable(false)] string procName, params SqlParameter[] parameters) => access.ExecProc<T, SqlParameter>(procName, parameters);

    public static int ExecuteProc(this IDbAccess access, string procName, params SqlParameter[] parameters) => access.ExecProc(procName, parameters);

    public static void ExecuteProcAndFill(this IDbAccess access, DataTable dataTable, string procName, params SqlParameter[] parameters)
    {
        access.ExecProcAndFill(dataTable, procName, parameters);
    }

    public static TResult ExecuteText<TResult>(this IDbAccess access, string sql, params SqlParameter[] parameters) => access.ExecText<TResult, SqlParameter>(sql, parameters);

    public static TResult ExecuteText<TResult>(this IDbAccess access, string sql, CommandBehavior behavior, params SqlParameter[] parameters) => access.ExecText<TResult, SqlParameter>(sql, behavior, parameters);

    public static int ExecuteText(this IDbAccess access, string sql, params SqlParameter[] parameters) => access.ExecText(sql, parameters);

    public static string RenameObjectToRunAsAutonomousTransaction(this IDbAccess access, string schema, object objectName)
    {
        var builder = new SqlConnectionStringBuilder(access.Session.Database.ConnectionString);
        var initialCatalog = builder.InitialCatalog;

        return $"loopback.[{initialCatalog}].[{schema}].[{objectName}]";
    }
}