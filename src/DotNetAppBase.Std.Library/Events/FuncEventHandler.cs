namespace DotNetAppBase.Std.Library.Events;

public delegate void FuncEventHandler<TParam, TResult>(object sender, FuncEventArgs<TParam, TResult> args);