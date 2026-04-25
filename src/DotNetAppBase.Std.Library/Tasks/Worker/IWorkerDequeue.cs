// ReSharper disable UnusedMember.Global
namespace DotNetAppBase.Std.Library.Tasks.Worker;

public interface IWorkerDequeue<T> : IWorker<T>
{
    void Keepalive();
}