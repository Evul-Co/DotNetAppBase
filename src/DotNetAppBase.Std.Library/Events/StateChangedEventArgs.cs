using System;
using DotNetAppBase.Std.Exceptions.Assert;

namespace DotNetAppBase.Std.Library.Events;

public class StateChangedEventArgs<TState> : EventArgs
{
    public StateChangedEventArgs(TState oldState, TState newState)
    {
        XContract.ArgIsNotNull(oldState, nameof(oldState));
        XContract.ArgIsNotNull(newState, nameof(newState));

        Old = oldState;
        New = newState;
    }

    public TState New { get; }

    public TState Old { get; }
}