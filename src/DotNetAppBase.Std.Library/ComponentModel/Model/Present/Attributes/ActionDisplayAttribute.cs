using System;
using DotNetAppBase.Std.Library.ComponentModel.Model.Interact.Contracts;
using DotNetAppBase.Std.Library.ComponentModel.Model.Interact.Enums;
using DotNetAppBase.Std.Library.ComponentModel.Model.Present.Attributes.Base;
using DotNetAppBase.Std.Library.ComponentModel.Model.Theme.Enums;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Present.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Enum | AttributeTargets.Field)]
public sealed class ActionDisplayAttribute : BaseDisplayAttribute, IPresentDisplay, IPresentImageConfig, IInteractionShortcutConfig
{
    private object _inherit;

    public object Inherit
    {
        get => _inherit;
        set
        {
            if (Equals(_inherit, value))
            {
                return;
            }

            _inherit = value;

            var fieldInfo = XHelper.Enums.GetFieldInfo(_inherit);

            var dispayConfig = XHelper.Models.GetDisplayData(fieldInfo);
            if (dispayConfig != null)
            {
                Name ??= dispayConfig.Value.Name;
                Description ??= dispayConfig.Value.Description;
            }

            var shortcut = XHelper.Reflections.Attributes.Get<IInteractionShortcutConfig>(fieldInfo);
            if (shortcut != null)
            {
                Alt = shortcut.Alt;
                Control = shortcut.Control;
                Shift = shortcut.Shift;
                Key = shortcut.Key;
            }

            var image = XHelper.Reflections.Attributes.Get<IPresentImageConfig>(fieldInfo);
            if (image != null)
            {
                Image = image.Image;
                ImagePath ??= image.ImagePath;
            }
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