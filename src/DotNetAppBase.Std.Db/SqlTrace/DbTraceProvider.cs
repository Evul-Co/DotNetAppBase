using System;
using System.Data.Common;

namespace DotNetAppBase.Std.Db.SqlTrace;

public class DbTraceProvider : object, IDisposable
{
    public static readonly DbTraceProvider Instance;

    private readonly object _objSync = new object();

    private bool _disposed;
    private bool _loaded;

    static DbTraceProvider()
    {
        Instance = new DbTraceProvider();
    }

    public bool AllowTrace { get; set; }

    public event DbTraceEventHandler SqlTrace;

    public void Trace(DbAccess access, DbCommand command)
    {
        if (!AllowTrace)
        {
            return;
        }

        if (!_loaded)
        {
            Load();
        }

        OnSqlTrace(access.Calller, command, access.Session.Database.ConnectionString);
    }

    private void Load()
    {
        lock (_objSync)
        {
            if (_loaded)
            {
                return;
            }

            DbTraceViewerLoader.Load();

            _loaded = true;
        }
    }

    private void OnSqlTrace(object sender, DbCommand command, string connectionString)
    {
        SqlTrace?.Invoke(sender, new DbTraceEventArgs(command, connectionString));
    }

    #region IDisposable

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            GC.SuppressFinalize(this);
        }

        if (AllowTrace && _loaded)
        {
            DbTraceViewerLoader.Unload();

            _loaded = false;
            AllowTrace = false;
        }

        _disposed = true;
    }

    ~DbTraceProvider()
    {
        Dispose(false);
    }

    #endregion
}