namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class Data
    {
        public static class Operations
        {
            public static string ExtrairRuc(string ruc) => ruc.Substring(0, ruc.Length - 1);

            public static string ExtrairRucDigito(string ruc) => ruc.Substring(ruc.Length - 2, 1);
        }
    }
}