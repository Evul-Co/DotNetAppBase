namespace DotNetAppBase.Std.Library.ComponentModel.Model.Present;

public interface IPresentableWithCode : IPresentable
{
    string Code { get; }
}