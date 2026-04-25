namespace DotNetAppBase.Std.Rmq.Abstraction
{
    public interface IRmqProxy
    {
        bool IsConnected { get; }
        bool Add(byte[] data, byte maxAttempts = 3);
        bool Add(object model, byte maxAttempts = 3);
        bool Connect();
        void Disconnect();
        void AddSubscriber(RmqSubscriber subscriber);
    }
}