namespace DotNetAppBase.Std.Rmq
{
    public class RmqQueueEndpoint
    {
        public string HostName { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string UserPwd { get; set; }

        public string QueueName { get; set; }

        public string Exchange { get; set; }
    }
}