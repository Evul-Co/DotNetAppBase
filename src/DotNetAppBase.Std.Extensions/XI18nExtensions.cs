using System;
using DotNetAppBase.Std.Library;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
public static class XI18nExtensions
// ReSharper restore CheckNamespace
// ReSharper restore InconsistentNaming
{
// ReSharper disable InconsistentNaming
    public static string DisplayAsI18n(this DateTime dateTime) => dateTime.ToString(XHelper.I18n.CurrentCulture);
    // ReSharper restore InconsistentNaming
}