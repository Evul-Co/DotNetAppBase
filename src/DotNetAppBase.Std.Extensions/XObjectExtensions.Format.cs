using System;
using System.Globalization;
using DotNetAppBase.Std.Exceptions.Contract;

// ReSharper disable CheckNamespace
public static partial class XObjectExtensions
// ReSharper restore CheckNamespace
{
    private const string NullDefaultDisplay = "<<NULL>>";

    public static string XDisplayAs(this DateTime dateTime, EDisplayAs displayAs)
    {
        switch (displayAs)
        {
            case EDisplayAs.Date:
                return dateTime.ToString("d", CultureInfo.CurrentCulture);

            case EDisplayAs.Time:
                return dateTime.ToString("t", CultureInfo.CurrentCulture);

            case EDisplayAs.DateTime:
                return dateTime.ToString("G", CultureInfo.CurrentCulture);

            default:
                throw XArgumentOutOfRangeException.Create(nameof(displayAs), displayAs, null);
        }
    }
}