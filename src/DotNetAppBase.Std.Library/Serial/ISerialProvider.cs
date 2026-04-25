using System;
using System.IO.Ports;
using DotNetAppBase.Std.Library.Events;

namespace DotNetAppBase.Std.Library.Serial;

public interface ISerialProvider : IDisposable
{
    event EventHandler<DataEventArgs<byte[]>> DataReceived;

    bool Close();

    void Configure(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits);

    long NewIdentity();

    bool Open();

    void Write(byte[] data);
}