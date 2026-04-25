using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using DotNetAppBase.Std.Library.Events;

namespace DotNetAppBase.Std.Library.Serial;

public class SerialProvider : ISerialProvider
{
    private readonly MemoryStream _buffer;
    private readonly SerialPort _serial;

    private bool _disposed;
    private long _identityID;

    public SerialProvider()
    {
        _serial = new SerialPort();
        _buffer = new MemoryStream();

        BindingEvents();
    }

    public bool IsOpen => _serial.IsOpen;

    public event EventHandler<DataEventArgs<byte[]>> DataReceived;

    public bool Close()
    {
        if (!IsOpen)
        {
            return false;
        }

        _serial.Close();

        return true;
    }

    public void Configure(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits)
    {
        _serial.PortName = portName;
        _serial.BaudRate = baudRate;
        _serial.DataBits = dataBits;
        _serial.Parity = parity;
        _serial.StopBits = stopBits;

        _serial.ReadTimeout = 500;
    }

    public long NewIdentity() => Interlocked.Increment(ref _identityID);

    public bool Open()
    {
        if (IsOpen)
        {
            return false;
        }

        _serial.Open();
        return true;
    }

    public void Write(byte[] data)
    {
        _serial.Write(data, 0, data.Length);
    }

    public event EventHandler BeginDelegateDataReceived;

    public event EventHandler EndDelegateDataReceived;

    private void BindingEvents()
    {
        _serial.DataReceived +=
            (sender, args) =>
                {
                    _buffer.SetLength(0);

                    try
                    {
                        var v = _serial.ReadLine();
                        Console.WriteLine(v);

                        int value;
                        while ((value = _serial.ReadByte()) != (byte) '\r')
                        {
                            _buffer.WriteByte((byte) value);
                        }

                        if (_buffer.Length > 0)
                        {
                            XHelper.Flow.Ensure(() => BeginDelegateDataReceived?.Invoke(this, EventArgs.Empty));

                            XHelper.Flow.Ensure(() => DataReceived?.Invoke(this, new DataEventArgs<byte[]>(_buffer.ToArray())));

                            XHelper.Flow.Ensure(() => EndDelegateDataReceived?.Invoke(this, EventArgs.Empty));
                        }
                    }
                    catch (TimeoutException)
                    {
                        DataReceived?.Invoke(this, new DataEventArgs<byte[]>(_buffer.ToArray()));
                    }
                    catch (Exception)
                    {
                        XHelper.Flow.Ensure(() => _serial.Close());
                    }
                };

        _serial.ErrorReceived +=
            (sender, args) => { XHelper.Flow.Ensure(() => _serial.Close()); };
    }

    #region IDisposable

    public void Dispose() => Dispose(true);

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        if (disposing)
        {
            GC.SuppressFinalize(this);
        }

        XHelper.Flow.Ensure(() => _serial.Close());
        _serial.Dispose();

        Disposed?.Invoke(this, EventArgs.Empty);
    }

    ~SerialProvider()
    {
        Dispose(false);
    }

    public event EventHandler Disposed;

    #endregion
}