using System;

namespace DotNetAppBase.Std.Library.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, Inherited = false)]
public sealed class HideMemberAttribute : Attribute { }