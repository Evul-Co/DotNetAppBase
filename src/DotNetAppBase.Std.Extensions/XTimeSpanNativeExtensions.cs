using System;
using System.ComponentModel;
using System.Text;
using DotNetAppBase.Std.Extensions.Properties;

// ReSharper disable CheckNamespace
public static class XTimeSpanNativeExtensions
// ReSharper restore CheckNamespace
{
    public static TimeSpan InDays(this int value) => TimeSpan.FromDays(value);

    public static TimeSpan InDays(this double value) => TimeSpan.FromDays(value);

    public static TimeSpan InHours(this int value) => TimeSpan.FromHours(value);

    public static TimeSpan InHours(this double value) => TimeSpan.FromHours(value);

    public static TimeSpan InMinutes(this int value) => TimeSpan.FromMinutes(value);

    public static TimeSpan InMinutes(this double value) => TimeSpan.FromMinutes(value);

    public static TimeSpan InSeconds(this int value) => TimeSpan.FromSeconds(value);

    public static TimeSpan InSeconds(this double value) => TimeSpan.FromSeconds(value);

    public static string ToDefaultDisplay(this TimeSpan? span) => span == null ? DbMessages.XTimeSpanNativeExtensions_FailOnCalculate : ToDefaultDisplay(span.Value);

    [Localizable(false)]
    public static string ToDefaultDisplay(this TimeSpan span)
    {
        var builder = new StringBuilder();

        double fraction;
        if (span.Days > 0)
        {
            builder.Append(
                $"{span.Days} dia{(span.Days > 1 ? "s" : string.Empty)} ");

            fraction = span.TotalHours - span.Days * 24;

            builder.Append(
                $"{fraction:f2} hora{(fraction > 1 ? "s" : string.Empty)}");
        }
        else
        {
            if (span.Hours > 0)
            {
                builder.Append(
                    $"{span.Hours} hora{(span.Hours > 1 ? "s" : string.Empty)} ");
            }

            if (span.TotalMinutes > 1)
            {
                fraction = span.TotalMinutes - span.Hours * 60;

                builder.Append(
                    $"{fraction:f1} minuto{(fraction > 1 ? "s" : string.Empty)}");
            }
            else
            {
                builder.Append("Menos de 1 minuto.");
            }
        }

        return builder.ToString();
    }
}