using System;
using DotNetAppBase.Std.Rmq.Events;

namespace DotNetAppBase.Std.Rmq.Abstraction
{
    public interface IRmqSubscriber
    {
        event EventHandler<RmqReceivedEventArgs> Received;
        void Initialize(RmqProxy proxy);
    }
}