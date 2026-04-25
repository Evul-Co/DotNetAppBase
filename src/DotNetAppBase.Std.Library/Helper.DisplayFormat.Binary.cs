using System.ComponentModel;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class DisplayFormat
    {
        [Localizable(false)]
        public static class Binary
        {
            public static string ByteToGigabyte(long length)
            {
                var mb = length / UnitOfMeasure.Gigabyte;

                return $"{mb:F} GB";
            }

            public static string ByteToKilobyte(long length)
            {
                var mb = length / UnitOfMeasure.Kilobyte;

                return $"{mb:F} KB";
            }

            public static string ByteToMegabyte(long length)
            {
                var mb = length / UnitOfMeasure.Megabyte;

                return $"{mb:F} MB";
            }

            public static string ByteToPetabyte(long length)
            {
                var mb = length / UnitOfMeasure.Petabyte;

                return $"{mb:F} PB";
            }

            public static string ByteToTerabyte(long length)
            {
                var mb = length / UnitOfMeasure.Terabyte;

                return $"{mb:F} TB";
            }
        }
    }
}