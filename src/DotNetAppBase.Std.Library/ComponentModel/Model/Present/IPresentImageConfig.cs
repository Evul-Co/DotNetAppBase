using DotNetAppBase.Std.Library.ComponentModel.Model.Theme.Enums;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Present;

public interface IPresentImageConfig
{
    EActionImage Image { get; }

    string ImagePath { get; }
}