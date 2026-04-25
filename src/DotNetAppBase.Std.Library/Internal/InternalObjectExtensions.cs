
// ReSharper disable CheckNamespace
internal static class InternalObjectExtensions
// ReSharper restore CheckNamespace
{
    public static T As<T>(this object obj) where T : class => obj as T;

    public static T CastTo<T>(this object value) => (T) value;

    public static bool Is<T>(this object obj) where T : class => obj is T;
}