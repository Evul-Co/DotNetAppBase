namespace DotNetAppBase.Std.Db.Contract;

public interface IDbAccessProvider
{
    IDbAccess GetAccess();
}