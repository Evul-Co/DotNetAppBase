using DotNetAppBase.Std.Library.ComponentModel.Model.Interact.Enums;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Interact.Contracts;

public interface IInteractionShortcutConfig
{
    bool Alt { get; }

    bool Control { get; }
    EKey Key { get; }

    bool Shift { get; }
}