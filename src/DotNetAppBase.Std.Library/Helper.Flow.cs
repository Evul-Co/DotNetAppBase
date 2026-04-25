namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static class Arrays
    {
        public static T[] Create<T>(params T[] args) => args;
        public static bool IsEmpty<T>(T[] array) => array.Length == 0;

        public static bool IsNullOrEmpty<T>(T[] array) => array == null || array.Length == 0;
    }
}