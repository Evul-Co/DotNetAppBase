using System;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public partial class Encodes
    {
        public static class Base64
        {
            public static byte[] FromBase64String(string str) => Convert.FromBase64String(str);

            public static string ToBase64String(byte[] bytes) => Convert.ToBase64String(bytes);
        }
    }
}