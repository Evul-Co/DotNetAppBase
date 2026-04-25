using System;
using DotNetAppBase.Std.Library.ComponentModel.Model.Interact.Contracts;
using DotNetAppBase.Std.Library.ComponentModel.Model.Interact.Enums;
using DotNetAppBase.Std.Library.ComponentModel.Model.Present.Attributes.Base;
using DotNetAppBase.Std.Library.ComponentModel.Model.Present.Reflection;
using DotNetAppBase.Std.Library.ComponentModel.Model.Theme.Enums;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Present.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public sealed class EnumDisplayAttribute : BaseDisplayAttribute, IPresentDisplay, IPresentImageConfig, IInteractionShortcutConfig
{
    private Enum _inherit;

    public Enum Inherit
    {
        get => _inherit;
        set
        {
            if (Equals(_inherit, value))
            {
                return;
            }

            _inherit = value;

            EnumReflectionHandler.Instance.Process(this, _inherit);
        }
    }

    public bool Alt { get; set; }

    public bool Control { get; set; }

    public EKey Key { get; set; }

    public bool Shift { get; set; }

    string IPresentDisplay.Description => GetDescription();

    string IPresentDisplay.GroupName => GetGroupName();

    string IPresentDisplay.Name => GetName();

    public EActionImage Image { get; set; } = EActionImage.None;

    public string ImagePath { get; set; }
}