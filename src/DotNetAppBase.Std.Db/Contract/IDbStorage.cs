namespace DotNetAppBase.Std.Db.Contract;

public interface IDbStorage
{
    IDbDatabase DefaultDatabase { get; set; }

    bool Constains(string name);

    IDbDatabase Restore(string name);

    bool Storage(IDbDatabase database);

    bool UnStorage(DbDatabase dataBase);
}