using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using DotNetAppBase.Std.Worker.Base;
using Microsoft.Extensions.Configuration;

namespace DotNetAppBase.Std.Worker.Net
{
    public abstract class UdpHostedService : HostedServiceBase
    {
        public IPAddress Address { get; set; }

        public int Port { get; private set; }

        protected abstract void DoWork(IPEndPoint endPoint, byte[] buffer);

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var client = new UdpClient(new IPEndPoint(Address, Port));

            while (!cancellationToken.IsCancellationRequested)
            {
                if (client.Available == 0)
                {
                    await Task.Delay(100, cancellationToken);

                    continue;
                }

                var endPoint = new IPEndPoint(IPAddress.Any, 0);
                var buffer = client.Receive(ref endPoint);

                if (buffer.Length == 0)
                {
                    continue;
                }

                DoWork(endPoint, buffer);
            }
        }

        protected override void InternalInitializeFromSettingsSection(IConfigurationSection settingSection)
        {
            base.InternalInitializeFromSettingsSection(settingSection);

            Address = IPAddress.Parse(settingSection.GetSection("EndPoint:Address").Value);
            Port = settingSection.GetSection("EndPoint:Port").Get<int>();
        }
    }
}