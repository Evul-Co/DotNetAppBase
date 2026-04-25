using System;

namespace DotNetAppBase.Std.Db.SqlTrace;

public interface IDbTraceViewerRegister : IDisposable
{
    DbTraceEventHandler DbTraceHandler { get; }
}