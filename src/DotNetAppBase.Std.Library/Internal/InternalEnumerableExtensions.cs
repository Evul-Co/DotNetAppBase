using System;
using System.Collections.Generic;
using DotNetAppBase.Std.Exceptions.Assert;
using DotNetAppBase.Std.Library;

// ReSharper disable CheckNamespace
internal static class InternalEnumerableExtensions
// ReSharper restore CheckNamespace
{
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        XContract.ArgIsNotNull(enumerable, nameof(enumerable));
        XContract.ArgIsNotNull(action, nameof(action));

        foreach (var item in enumerable)
        {
            action(item);
        }
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => XHelper.Enumerable.IsNullOrEmpty(enumerable);

    public static T[] ToArrayEfficient<T>(this IEnumerable<T> values) => XHelper.Enumerable.ToArrayEfficient(values);
}