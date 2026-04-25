using System.Data;
using System.Data.Common;
using DotNetAppBase.Std.Db.Contract;
#if NETFRAMEWORK
using System.Data.SqlClient;

#else
using Microsoft.Data.SqlClient;
#endif

namespace DotNetAppBase.Std.Db.SqlServer;

public class SqlServerSession : DbSession
{
    public SqlServerSession(IDbDatabase dbServerDatabase) : base(dbServerDatabase) { }

    public override DbDataAdapter CreateDataAtapter(DbCommand cmd) => new SqlDataAdapter(cmd.CastTo<SqlCommand>());

    public override DbParameter CreateReturnParameter() => new SqlParameter {ParameterName = "@RETURN_VALUE", Direction = ParameterDirection.ReturnValue};

    public override bool RetryInteractionOnDbExcepion(DbException exception) => SqlServerExceptionHandler.RetryInteraction(Database, exception.CastTo<SqlException>());
}