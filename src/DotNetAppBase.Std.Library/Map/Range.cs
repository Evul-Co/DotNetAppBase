using System.Collections;
using System.Collections.Generic;

namespace DotNetAppBase.Std.Library.Map;

public struct Range : IEnumerable<int>
{
    public Range(int min, int max)
    {
        Min = min;
        Max = max;
    }

    public int Min { get; }

    public int Max { get; }

    public bool Reverse => Min > Max;

    public bool In(int value) => Reverse ? Max <= value && Min >= value : Min <= value && Max >= value;

    public IEnumerator<int> GetEnumerator()
    {
        if (Reverse)
        {
            for (var i = Max; i <= Min; i++)
            {
                yield return i;
            }
        }
        else
        {
            for (var i = Min; i <= Max; i++)
            {
                yield return i;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}