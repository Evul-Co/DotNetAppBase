using System;

// ReSharper disable UnusedMember.Global

namespace DotNetAppBase.Std.Library.Tasks.Worker;

public class WorkerDequeue<T> : Worker<T>, IWorkerDequeue<T>
{
    public void Keepalive()
    {
        Start();
    }

    protected override TimeSpan? InternalGetFrequency() => null;
}