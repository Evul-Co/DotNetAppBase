using System.IO;
using System.Linq;
using System.Reflection;

namespace DotNetAppBase.Std.Db.SqlTrace;

public static class DbTraceViewerLoader
{
    public const string AssemblyPattern = "DotNetAppBase.Std.Db.TraceViewer*.dll";

    private static IDbTraceViewerRegister _register;

    public static void Load()
    {
        if (_register != null)
        {
            return;
        }

        var files = Directory.GetFiles(Directory.GetCurrentDirectory(), AssemblyPattern, SearchOption.TopDirectoryOnly);

        foreach (var assembly in files.Select(Assembly.LoadFile))
        {
            _register = assembly.CreateInstance("DbTraceViewerRegister") as IDbTraceViewerRegister;
            if (_register != null)
            {
                DbTraceProvider.Instance.SqlTrace += _register.DbTraceHandler;
            }
        }
    }

    public static void Unload()
    {
        if (_register == null)
        {
            return;
        }

        _register.Dispose();

        DbTraceProvider.Instance.SqlTrace -= _register.DbTraceHandler;
    }
}