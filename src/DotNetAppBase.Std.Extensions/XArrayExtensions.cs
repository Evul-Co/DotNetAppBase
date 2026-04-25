using System;
using DotNetAppBase.Std.Library;

// ReSharper disable CheckNamespace
public static class XArrayExtensions
// ReSharper restore CheckNamespace
{
    public static void ForEach<T>(this T[] array, Action<T, int> action)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        for (var i = 0; i < array.Length; i++)
        {
            action(array[i], i);
        }
    }

    public static bool IsEmpty<T>(this T[] array) => XHelper.Arrays.IsEmpty(array);

    public static bool IsNullOrEmpty<T>(this T[] array) => XHelper.Arrays.IsNullOrEmpty(array);

    public static bool IsThere<T>(this T[] array, int index) => array != null && index >= 0 && index <= array.Length - 1;

    public static bool TryGet<T>(this T[] array, int index, out T item)
    {
        if (!array.IsThere(index))
        {
            item = default;
            return false;
        }

        item = array[index];
        return true;
    }
}