using System;

namespace DotNetAppBase.Std.Library.ComponentModel.Model.Abstraction;

public interface IDateRange
{
    bool IsHasRangeValues { get; }

    bool IsNull { get; }

    bool IsNullMax { get; }

    bool IsNullMin { get; }

    bool IsNullPartial { get; }

    DateTime? Max { get; }

    DateTime? MaxAsEndDay { get; }
    DateTime? Min { get; }

    DateTime? MinAsBeginDay { get; }

    TimeSpan Range { get; }
}