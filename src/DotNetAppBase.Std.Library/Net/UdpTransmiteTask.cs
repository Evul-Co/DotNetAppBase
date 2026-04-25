using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DotNetAppBase.Std.Library.Tasks;
using DotNetAppBase.Std.Library.Tasks.Threading;

namespace DotNetAppBase.Std.Library.Net;

public class UdpTransmiteTask : XTask
{
    private readonly BlockingCollection<Item> _data;
    private readonly IDestinationProvider _destinationProvider;

    private readonly Socket _socket;

    private CancellationTokenSource _cancellationTokenSource;

    public UdpTransmiteTask(IDestinationProvider destinationProvider)
    {
        _destinationProvider = destinationProvider;

        _cancellationTokenSource = new CancellationTokenSource();

        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        _data = new BlockingCollection<Item>();

        AutoCatchException = false;
    }

    public void Add(byte[] datagrama)
    {
        if (IsStopping)
        {
            return;
        }

        _data.Add(new Item {Datagrama = datagrama});
    }

    protected override void InternalExecute()
    {
        var item = _data.Take(_cancellationTokenSource.Token);

        foreach (var ep in _destinationProvider.Get())
        {
            try
            {
                _socket.SendTo(item.Datagrama, ep);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    protected override TimeSpan? InternalGetFrequency() => null;

    protected override void InternalWaitComplete(ref bool waitComplete, XTimeout tOut)
    {
        base.InternalWaitComplete(ref waitComplete, tOut);

        if (waitComplete && tOut.Keep())
        {
            while (_data.Count > 0 && tOut.Keep())
            {
                Thread.Sleep(100);
            }
        }

        waitComplete = false;

        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();
    }

    private class Item
    {
        public byte[] Datagrama { get; set; }
    }

    public interface IDestinationProvider
    {
        IEnumerable<EndPoint> Get();
    }
}