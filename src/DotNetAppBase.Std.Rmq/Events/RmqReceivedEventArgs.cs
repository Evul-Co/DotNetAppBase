using System;
using System.Text;
using System.Text.Json;

namespace DotNetAppBase.Std.Rmq.Events
{
    public class RmqReceivedEventArgs
    {
        public RmqReceivedEventArgs(ulong key, ReadOnlyMemory<byte> body)
        {
            Key = key;
            Body = body;
        }

        public ulong Key { get; }

        public ReadOnlyMemory<byte> Body { get; }

        public T Deserialize<T>()
        {
            var data = Encoding.UTF8.GetString(Body.ToArray());

            return JsonSerializer.Deserialize<T>(data);
        }
    }
}