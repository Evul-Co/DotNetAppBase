using System;

namespace DotNetAppBase.Std.Library.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class HideTypeAttribute : Attribute { }