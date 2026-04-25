namespace DotNetAppBase.Std.Library.ComponentModel.Model.Present;

public interface IDisplayType
{
    string Description { get; }
    string DisplayPattern { get; }

    int Level { get; }

    string Name { get; }
}