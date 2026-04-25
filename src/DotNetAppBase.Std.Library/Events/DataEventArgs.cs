using System;

namespace DotNetAppBase.Std.Library.Events;

public class DataEventArgs<TData> : EventArgs
{
    public DataEventArgs(TData data, string additionalData = null)
    {
        Data = data;
        AdditionalData = additionalData;
    }

    public string AdditionalData { get; }

    public TData Data { get; }
}