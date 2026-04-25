using System;
using System.Collections.Generic;
using System.Reflection;
using DotNetAppBase.Std.Library;

// ReSharper disable CheckNamespace
public static class XReflectionExtensions
// ReSharper restore CheckNamespace
{
    public const BindingFlags InstanceBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
    public const BindingFlags StaticBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

    public static IEnumerable<MethodInfo> Get(this Func<BindingFlags, MethodInfo[]> funcGet) => funcGet(BindingFlags.Default);

    public static string GetDescription(this PropertyInfo propertyInfo) => XHelper.Models.GetDescription(propertyInfo);

    public static string GetDisplayName(this PropertyInfo propertyInfo) => XHelper.Models.GetDisplayName(propertyInfo);

    public static Func<BindingFlags, MethodInfo[]> Instance(this Func<BindingFlags, MethodInfo[]> funcGet) => flags => funcGet(flags | BindingFlags.Instance);

    public static Func<BindingFlags, MethodInfo[]> Methods(this Type type) => type.GetMethods;

    public static Func<BindingFlags, MethodInfo[]> Public(this Func<BindingFlags, MethodInfo[]> funcGet) => flags => funcGet(flags | BindingFlags.Public);
}