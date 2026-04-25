namespace DotNetAppBase.Std.Library.ComponentModel.Model.Abstraction;

public interface IKey
{
    object Key { get; }
}

public interface IKey<out TKey> : IKey
{
    new TKey Key { get; }
}