namespace DotNetAppBase.Std.Library.ComponentModel.Model.Present;

public interface IPresentableValue : IPresentable
{
    decimal Value { get; }
}