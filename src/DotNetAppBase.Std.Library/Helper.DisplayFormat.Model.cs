namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class DisplayFormat
    {
        public static class Model
        {
            public const string DefaultPrimaryKeyColumnName = "ID";
            public const string DefaultKeyFormat = "000000000";

            public static string AsKey(int value) => value.ToString(DefaultKeyFormat);
        }
    }
}