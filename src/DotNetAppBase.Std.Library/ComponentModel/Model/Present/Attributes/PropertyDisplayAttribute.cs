using System;
using DotNetAppBase.Std.Library.ComponentModel.Model.Present.Attributes.Base;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Present.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class PropertyDisplayAttribute : BaseDisplayAttribute, IPresentDisplay
{
    string IPresentDisplay.Description => GetDescription();

    string IPresentDisplay.Name => GetName();

    string IPresentDisplay.GroupName => GetGroupName();
}