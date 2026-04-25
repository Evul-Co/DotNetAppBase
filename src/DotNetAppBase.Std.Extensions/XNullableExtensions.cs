using System;
using DotNetAppBase.Std.Exceptions.Assert;
using JetBrains.Annotations;

// ReSharper disable CheckNamespace
public static class XNullableExtensions
// ReSharper restore CheckNamespace
{
    [ContractAnnotation("nullableValue: null <= false")]
    public static T ReadValue<T>(this T? nullableValue) where T : struct
    {
        XContract.Assert(nullableValue.HasValue, $"O valor não pode ser lido porque o {nameof(Nullable<T>)} era nulo.");

        return nullableValue.Value;
    }

    public static T ReadValue<T>(this T? nullableValue, T defaultValue) where T : struct => nullableValue ?? defaultValue;

    public static T ReadValueOrDefault<T>(this T? nullableValue) where T : struct => nullableValue ?? default;
}