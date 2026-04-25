using System;
using DotNetAppBase.Std.Library.I18n;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Present.Attributes.Base;

public class BaseDisplayAttribute : Attribute, IDisplayType
{
    private readonly LocalizableString _description = new LocalizableString(nameof(Description));
    private readonly LocalizableString _groupName = new LocalizableString(nameof(GroupName));
    private readonly LocalizableString _name = new LocalizableString(nameof(Name));

    private string _displayPattern;
    private Type _resourceType;

    public string Description
    {
        get => _description.Value;
        set
        {
            if (_description.Value != value)
            {
                _description.Value = value;
            }
        }
    }

    public string GroupName
    {
        get => _groupName.Value;
        set
        {
            if (_groupName.Value != value)
            {
                _groupName.Value = value;
            }
        }
    }

    public string Name
    {
        get => _name.Value;
        set
        {
            if (_name.Value != value)
            {
                _name.Value = value;
            }
        }
    }

    public Type ResourceType
    {
        get => _resourceType;
        set
        {
            if (_resourceType == value)
            {
                return;
            }

            _resourceType = value;

            _name.ResourceType = value;
            _description.ResourceType = value;
            _groupName.ResourceType = value;
        }
    }

    public override object TypeId => this;

    public virtual int Level { get; set; }

    public string DisplayPattern
    {
        get => _displayPattern;
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                // ReSharper disable LocalizableElement
                if (!value.Contains("{"))
                {
                    value = "{" + value + "}";
                }
                // ReSharper restore LocalizableElement
            }

            _displayPattern = value;
        }
    }

    string IDisplayType.Description => GetDescription();

    string IDisplayType.Name => GetName();

    public string GetDescription() => _description.GetLocalizableValue();

    public string GetGroupName() => _groupName.GetLocalizableValue();

    public string GetName() => _name.GetLocalizableValue();
}