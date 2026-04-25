using System;
using System.ComponentModel;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static class Bind
    {
        public static string GetDefaultBindingProperty(Type type) => Reflections.Attributes.Get<DefaultBindingPropertyAttribute>(type)?.Name;

        public static DefaultBindingPropertyAttribute GetDefaultBindingPropertyAttribute(Type type) => Reflections.Attributes.Get<DefaultBindingPropertyAttribute>(type);
    }
}