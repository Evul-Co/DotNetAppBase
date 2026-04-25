using System;

namespace DotNetAppBase.Std.Library.ComponentModel.Attributes.ViewDesign;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ViewAttribute : Attribute
{
    public ViewAttribute(string id)
    {
        ID = Guid.Parse(id);
    }

    protected ViewAttribute() { }

    public Guid? ID { get; }
}