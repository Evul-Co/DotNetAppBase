using DotNetAppBase.Std.Library;

// ReSharper disable CheckNamespace
public static class XNumeric
// ReSharper restore CheckNamespace
{
    public static string CurrencySymbol => XHelper.DisplayFormat.Financial.CurrencySymbol;

    public static decimal AsMoney(this decimal value) => XHelper.Data.Numeric.Round(value, 2);

    public static string DisplayAsCurrency(this decimal value) => XHelper.DisplayFormat.Financial.AsCurrency(value);

    public static string DisplayAsCurrency(this double value) => XHelper.DisplayFormat.Financial.AsCurrency((decimal) value);

    public static string FormatAsCurrency(this decimal value) => XHelper.DisplayFormat.Financial.Format(value);

    public static string FormatAsCurrency(this double value) => XHelper.DisplayFormat.Financial.Format((decimal) value);

    public static decimal XRound(this decimal value, int decimalPlaces) => XHelper.Data.Numeric.Round(value, decimalPlaces);

    public static decimal? XRound(this decimal? value, int decimalPlaces) => XHelper.Data.Numeric.Round(value, decimalPlaces);
}