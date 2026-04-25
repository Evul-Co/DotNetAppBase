using System;
using System.Data.Common;
using DotNetAppBase.Std.Db.Contract;
using DotNetAppBase.Std.Db.Enums;
using DotNetAppBase.Std.Library;

namespace DotNetAppBase.Std.Db;

public abstract class DbDatabase : IDbDatabase
{
    private IDbSession _defaultSession;

    protected DbDatabase(string name, int commandTimeout = 60)
    {
        Name = name;

        CommandTimeout = commandTimeout;
    }

    public IDbDateTimeProvider DbDateTimeProvider { get; set; }

    public abstract int ConnectionTimeout { get; set; }

    public int CommandTimeout { get; set; }

    public string ConnectionString => InternalGetConnectionString();

    public IDbSession DefaultSession
    {
        get => _defaultSession ??= InternalCreateSqlSession();
        internal set => _defaultSession = value;
    }

    public string Name { get; }

    public EDatabaseOption Options { get; set; }

    public bool CheckConnection(out string error)
    {
        try
        {
            InternalGetDateTime();

            error = null;

            return true;
        }
        catch (DbException ex)
        {
            error = XHelper.Exceptions.GetMessageOnTopOfStack(ex);

            return false;
        }
    }

    public DbAccess GetAccess() => DefaultSession.GetAccess().CastTo<DbAccess>();

    public DateTime GetDateTime() => InternalGetDateTime();

    public DbConnection NewConnection() => InternalCreateConnection(ConnectionString);

    public IDbSession NewSession() => InternalCreateSqlSession();

    protected abstract DbConnection InternalCreateConnection(string connectionString);

    protected abstract IDbSession InternalCreateSqlSession();

    protected abstract string InternalGetConnectionString();

    protected abstract DateTime InternalGetDateTime(int connectionTimeout = 5);
}