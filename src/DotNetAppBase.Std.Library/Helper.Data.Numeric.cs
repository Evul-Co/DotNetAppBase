using System;
using System.ComponentModel;
using System.Globalization;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class Data
    {
        [Localizable(false)]
        public static class Numeric
        {
            public static decimal Round(decimal value, int decimalPlaces)
            {
                var valorNovo = decimal.Round(value, decimalPlaces);
                var valorNovoStr = valorNovo.ToString("F" + decimalPlaces, CultureInfo.CurrentCulture);

                return decimal.Parse(valorNovoStr);
            }

            public static decimal? Round(decimal? value, int decimalPlaces)
            {
                if (value == null)
                {
                    return null;
                }

                return Round(value.Value, decimalPlaces);
            }
        }
    }
}