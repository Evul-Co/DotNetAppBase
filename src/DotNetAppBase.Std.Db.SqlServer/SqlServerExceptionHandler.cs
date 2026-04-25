using DotNetAppBase.Std.Db.Contract;
using DotNetAppBase.Std.Db.Enums;
#if NETFRAMEWORK
using System.Data.SqlClient;

#else
using Microsoft.Data.SqlClient;
#endif

namespace DotNetAppBase.Std.Db.SqlServer;

public static class SqlServerExceptionHandler
{
    public const int TimeoutErrorCode = 1222;
    public const int DeadlockErrorCode = 1205;

    public const int TransportLevelErrorCode = 121;
    public const int TransportLevelSeverityClass = 20;

    public static bool RetryInteraction(IDbDatabase database, SqlException exception)
    {
        switch (exception.Number)
        {
            case TimeoutErrorCode:
            case -2:
                return (database.Options & EDatabaseOption.RetryTimeout) == EDatabaseOption.RetryTimeout;

            case DeadlockErrorCode:
                return (database.Options & EDatabaseOption.RetryDeadlock) == EDatabaseOption.RetryDeadlock;

            case TransportLevelErrorCode when exception.Class == TransportLevelSeverityClass:
                SqlConnection.ClearAllPools();

                return true;

            default:
                return false;
        }
    }
}