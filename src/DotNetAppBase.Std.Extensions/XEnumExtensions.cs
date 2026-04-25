using System;
using DotNetAppBase.Std.Library;

// ReSharper disable UnusedMember.Global

// ReSharper disable CheckNamespace
public static class XEnumExtensions
// ReSharper restore CheckNamespace
{
    public static string DisplayEnumAsDescription<TEnum>(this TEnum enumValue) => XHelper.Enums.GetDisplay(enumValue, enumValue.ToString());

    public static bool IsDefined<TEnum>(this TEnum enumValue) => Enum.IsDefined(typeof(TEnum), enumValue);

    public static bool IsIn<TEnum>(this TEnum enumValue, params TEnum[] enums) where TEnum : Enum => XHelper.Enums.IsIn(enumValue, enums);

    public static bool IsLast<TEnum>(this TEnum enumValue)
    {
        if (!Enum.IsDefined(typeof(TEnum), enumValue))
        {
            return false;
        }

        var values = Enum.GetValues(typeof(TEnum));

        return Array.IndexOf(values, enumValue) + 1 == values.Length;
    }

    public static TEnum Next<TEnum>(this TEnum enumValue)
    {
        if (!Enum.IsDefined(typeof(TEnum), enumValue))
        {
            return enumValue;
        }

        var values = Enum.GetValues(typeof(TEnum));
        var index = Array.IndexOf(values, enumValue);

        if (index + 1 == values.Length)
        {
            return enumValue;
        }

        return (TEnum) values.GetValue(index + 1);
    }
}