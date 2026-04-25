using System;

namespace DotNetAppBase.Std.Library.Guids;

public class XGuidAttribute : Attribute
{
    public XGuidAttribute(string guid)
    {
        Value = Guid.Parse(guid);
    }

    public Guid Value { get; }
}