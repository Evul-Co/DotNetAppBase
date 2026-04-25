using System;
using DotNetAppBase.Std.Library.ComponentModel.Model.Abstraction;
using DotNetAppBase.Std.Library.Properties;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Primitives;

public struct DateRange : IDateRange
{
    public DateRange(DateTime? min, DateTime? max)
    {
        Min = min;
        Max = max;
    }

    public DateTime? Min { get; }

    public DateTime? Max { get; }

    public DateTime? MinAsBeginDay => Min?.Date;

    public DateTime? MaxAsEndDay => Max?.Date.AddDays(1).AddSeconds(-1);

    public TimeSpan Range
    {
        get
        {
            if (!IsHasRangeValues)
            {
                return TimeSpan.Zero;
            }

            // ReSharper disable PossibleInvalidOperationException
            return Max.Value - Min.Value;
            // ReSharper restore PossibleInvalidOperationException
        }
    }

    public bool IsNullMin => Min == null;

    public bool IsNullMax => Max == null;

    public bool IsNull => IsNullMin && IsNullMax;

    public bool IsNullPartial => IsNullMin || IsNullMax;

    public bool IsHasRangeValues => !IsNullMin && !IsNullMax;

    public override bool Equals(object obj) => obj is DateRange range && this == range;

    public override int GetHashCode() => Min.GetHashCode() ^ Max.GetHashCode();

    public static bool operator ==(DateRange dr1, DateRange dr2) => dr1.Min == dr2.Min && dr1.Max == dr2.Max;

    public static bool operator !=(DateRange dr1, DateRange dr2) => !(dr1 == dr2);

    public DateRange UpdateMin(DateTime? min) => new DateRange(min, Max);

    public DateRange UpdateMax(DateTime? max) => new DateRange(Min, max);

    public override string ToString()
    {
        if (IsNull)
        {
            return DbMessages.DateRange_ToString__Não_Definido_;
        }

        if (!IsNullPartial)
        {
            return string.Format(DbMessages.DateRange_ToString_maior_ou_igual__0__e_menor_ou_gual__1_, Min, Max);
        }

        return IsNullMin
            ? string.Format(DbMessages.DateRange_ToString_maior_ou_igual_a__0_, Max)
            : string.Format(DbMessages.DateRange_ToString_menor_ou_igual_a__0_, Min);
    }
}