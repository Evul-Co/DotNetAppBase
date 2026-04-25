using System;
using DotNetAppBase.Std.Library.ComponentModel.Model.Present.Attributes.Base;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Present.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class TypeDisplayAttribute : BaseDisplayAttribute, IPresentDisplay
{
    string IPresentDisplay.Description => GetDescription();

    string IPresentDisplay.Name => GetName();

    string IPresentDisplay.GroupName => GetGroupName();
}