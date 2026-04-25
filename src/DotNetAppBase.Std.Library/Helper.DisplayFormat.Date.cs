using System;
using System.ComponentModel;
using System.Globalization;
using DotNetAppBase.Std.Library.Properties;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static partial class DisplayFormat
    {
        public static class Date
        {
            public static string Format(TimeSpan span)
            {
                var hours = (int) span.TotalHours;
                var minutes = (int) (span.TotalMinutes - hours * 60);
                var seconds = (int) (span.TotalSeconds - (int) span.TotalMinutes * 60);

                if (hours > 0)
                {
                    if (minutes > 0 && seconds > 0)
                    {
                        return string.Format(DbMessages.Date_Format1, hours, minutes, seconds);
                    }

                    if (minutes > 0)
                    {
                        return string.Format(DbMessages.Date_Format2, hours, minutes);
                    }

                    if (seconds > 0)
                    {
                        return string.Format(DbMessages.Date_Format3, hours, seconds);
                    }

                    return string.Format(DbMessages.Date_Format4, hours);
                }

                if (minutes > 0 && seconds > 0)
                {
                    return string.Format(DbMessages.Date_Format5, minutes, seconds);
                }

                if (minutes > 0)
                {
                    return string.Format(DbMessages.Date_Format6, minutes);
                }

                return string.Format(DbMessages.Date_Format7, seconds);
            }

            [Localizable(false)]
            public static string FormatAsLongDate(DateTime dateTime) => dateTime.ToString("G", CultureInfo.CurrentCulture);

            [Localizable(false)]
            public static string FormatAsShortDate(DateTime dateTime) => dateTime.ToString("d", CultureInfo.CurrentCulture);

            [Localizable(false)]
            public static string FormatSmall(double seconds, bool showSeconds = true)
            {
                var span = TimeSpan.FromSeconds(seconds);

                return
                    showSeconds
                        ? $"{span.Days * 24 + span.Hours}:{span.Minutes:D2}:{span.Seconds:D2}"
                        : $"{span.Days * 24 + span.Hours}:{span.Minutes:D2}";
            }

            public static string FormatSmall(TimeSpan span, bool showSeconds = true) => FormatSmall(span.TotalSeconds, showSeconds);
        }
    }
}