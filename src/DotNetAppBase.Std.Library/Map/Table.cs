using System;
using System.Collections.Generic;

namespace DotNetAppBase.Std.Library.Map;

public static class Table
{
    public static IEnumerable<Row<Tuple<T, T, T>>> DistributeThreeColumns<T>(IEnumerable<T> data)
    {
        var arr = XHelper.Enumerable.ToArrayEfficient(data);

        var range = new Range(0, arr.Length);

        var rowIndex = 0;
        for (var i = 0; range.In(i);)
        {
            yield return new Row<Tuple<T, T, T>>
                {
                    Index = rowIndex++,
                    Data = new Tuple<T, T, T>(arr[i++], range.In(i) ? arr[i++] : default, range.In(i) ? arr[i++] : default)
                };
        }
    }

    public static IEnumerable<Row<Tuple<T, T>>> DistributeTwoColumns<T>(IEnumerable<T> data)
    {
        var arr = XHelper.Enumerable.ToArrayEfficient(data);

        var range = new Range(0, arr.Length - 1);

        var rowIndex = 0;
        for (var i = 0; range.In(i);)
        {
            yield return new Row<Tuple<T, T>>
                {
                    Index = rowIndex++,
                    Data = new Tuple<T, T>(arr[i++], range.In(i) ? arr[i++] : default)
                };
        }
    }
}