using System;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Present.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class LookupDisplayAttribute : PropertyDisplayAttribute
{
    public LookupDisplayAttribute() { }

    public LookupDisplayAttribute(int visibleIndex)
    {
        VisibleIndex = visibleIndex;
    }

    public bool ConfigureAsDisplayName { get; set; }

    public int VisibleIndex { get; set; }
}