using System;
using System.Threading;
using System.Threading.Tasks;
using DotNetAppBase.Std.Exceptions.Assert;
using DotNetAppBase.Std.Exceptions.Base;

namespace DotNetAppBase.Std.Library.Tasks.Worker;

public class Worker<T> : IWorker<T>
{
    // ReSharper disable StaticMemberInGenericType
    public static readonly TimeSpan DefaultFrequency = TimeSpan.FromSeconds(2);
    // ReSharper restore StaticMemberInGenericType

    private readonly object _syncRunning = new object();

    private readonly ManualResetEvent _waitStop;

    private bool _active;

    private bool _disposed;
    private Action<T> _processData;
    private Func<T> _readData;

    private bool _stopping;

    public Worker()
    {
        _waitStop = new ManualResetEvent(false);
    }

    public TimeSpan Frequency { get; set; } = DefaultFrequency;

    public bool IsContinuous => InternalGetFrequency() != null;

    public bool AutoCatchException { get; set; } = true;

    public bool Enabled
    {
        get
        {
            lock (_syncRunning)
            {
                return _active;
            }
        }
    }

    public string Name { get; private set; }

    public void Configure(string workerName, Func<T> readData, Action<T> processData)
    {
        if (Enabled)
        {
            throw new XException("Can't register methods call when worker is running.");
        }

        Name = workerName ?? throw new ArgumentNullException(nameof(workerName));
        _readData = readData ?? throw new ArgumentNullException(nameof(readData));
        _processData = processData ?? throw new ArgumentNullException(nameof(processData));
    }

    public bool Start()
    {
        InternalCheckIfCanRunTask();

        lock (_syncRunning)
        {
            if (!_active)
            {
                _active = true;
                _waitStop.Reset();

                InternalRunTask();

                return true;
            }

            return false;
        }
    }

    public async Task<bool> Stop(TimeSpan? timeout = null, bool waitComplete = false)
    {
        lock (_syncRunning)
        {
            if (!_active || _stopping)
            {
                return false;
            }

            _stopping = true;
        }

        return await Task.Run(() => _waitStop.WaitOne());
    }

    protected void InternalCheckIfCanRunTask()
    {
        if (_readData == null)
        {
            throw new XException("Can't run task withou methods call registered.");
        }
    }

    protected virtual TimeSpan? InternalGetFrequency() => Frequency;

    protected void InternalRunTask()
    {
        Task.Run(
            () =>
                {
                    Thread.CurrentThread.Name = Name;

                    var item = default(T);
                    do
                    {
                        try
                        {
                            item = _readData();

                            if (item != null)
                            {
                                _processData(item);
                            }
                        }
                        catch (Exception ex)
                        {
                            XDebug.OnException(ex);

                            if (!AutoCatchException)
                            {
                                throw;
                            }
                        }

                        if (IsContinuous)
                        {
                            // ReSharper disable PossibleInvalidOperationException
                            var waitForSeconds = (int) Math.Max(InternalGetFrequency().Value.TotalSeconds, 1);
                            // ReSharper restore PossibleInvalidOperationException

                            for (var i = 0; i < waitForSeconds; i++)
                            {
                                Thread.Sleep(1000);

                                if (_stopping)
                                {
                                    break;
                                }
                            }
                        }
                    } while ((!Equals(item, null) || IsContinuous) && !_stopping);

                    lock (_syncRunning)
                    {
                        _active = false;
                        _stopping = false;

                        _waitStop.Set();
                    }
                });
    }

    #region IDisposable

    ~Worker()
    {
        Dispose(false);
    }

    private async void Dispose(bool disposing)
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

        await Stop();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    #endregion
}