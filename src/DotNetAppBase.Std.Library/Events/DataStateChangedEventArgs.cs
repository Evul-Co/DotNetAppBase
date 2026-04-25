namespace DotNetAppBase.Std.Library.Events;

public class DataStateChangedEventArgs<TData, TState> : DataEventArgs<TData>
{
    public DataStateChangedEventArgs(TData data, TState state) : this(data, state, null) { }

    public DataStateChangedEventArgs(TData data, TState state, string additionalData) : base(data, additionalData)
    {
        State = state;
    }

    public TState State { get; }
}