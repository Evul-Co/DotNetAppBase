using System.Threading;

// ReSharper disable UnusedMember.Global

namespace DotNetAppBase.Std.Library.Data;

public class IdentityGenerator
{
    private long _seed;

    public long Current => Interlocked.Read(ref _seed);

    public long Get() => Interlocked.Increment(ref _seed);

    public int GetInt32() => (int) Interlocked.Increment(ref _seed);
}