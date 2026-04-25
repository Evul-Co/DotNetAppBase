using System;
using System.Collections.Generic;
using System.Linq;
using DotNetAppBase.Std.Db.Contract;

namespace DotNetAppBase.Std.Db;

public sealed class DbStorage : IDbStorage
{
    public static readonly DbStorage Instance;

    private readonly Dictionary<string, IDbDatabase> _dbs;

    static DbStorage()
    {
        Instance = new DbStorage();
    }

    private DbStorage()
    {
        _dbs = new Dictionary<string, IDbDatabase>();
    }

    public IDbDatabase DefaultDatabase { get; set; }

    public bool Constains(string name) => !string.IsNullOrEmpty(name) && _dbs.ContainsKey(name);

    public IDbDatabase Restore(string name) => Constains(name) ? _dbs[name] : null;

    public bool Storage(IDbDatabase database)
    {
        if (Constains(database.Name))
        {
            return false;
        }

        _dbs.Add(database.Name, database);

        return true;
    }

    public bool UnStorage(DbDatabase dataBase)
    {
        if (!Constains(dataBase.Name) || Restore(dataBase.Name) != dataBase)
        {
            return false;
        }

        _dbs.Remove(dataBase.Name);

        return true;
    }
}