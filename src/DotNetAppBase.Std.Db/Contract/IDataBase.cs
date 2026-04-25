namespace DotNetAppBase.Std.Db.Contract;

public interface IDatabase : IDbDateTimeProvider
{
    bool CheckConnection(out string error);
}