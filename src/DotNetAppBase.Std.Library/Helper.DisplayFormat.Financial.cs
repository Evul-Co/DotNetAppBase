using System.ComponentModel;
using System.Globalization;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class DisplayFormat
    {
        public static class Financial
        {
            public static string CurrencySymbol => CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol;

            [Localizable(false)]
            public static string AsCurrency(decimal value) => value.ToString("C");

            [Localizable(false)]
            public static string Format(decimal value) => value.ToString("F2");
        }
    }
}