using System.Collections.Concurrent;

// ReSharper disable CheckNamespace
public static class XConcurrentQueueExtensions
// ReSharper restore CheckNamespace
{
    public static void Clear<T>(this ConcurrentQueue<T> queue)
    {
        while (queue.TryDequeue(out _)) { }
    }
}