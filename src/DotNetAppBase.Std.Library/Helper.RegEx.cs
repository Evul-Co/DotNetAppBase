using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static class RegEx
    {
        public static IEnumerable<FindInfo> Matches(string openToken, string closeToken, string origin)
        {
            var e = new Regex($@"{openToken}(.+){closeToken}");

            var matchCollection = e.Matches(origin);

            return
                matchCollection
                    .Cast<Match>()
                    .Select(
                        match =>
                            new FindInfo
                                {
                                    Expression = match.Value,
                                    Content = match.Groups.Cast<Capture>().LastOrDefault()?.Value,
                                    Index = match.Index,
                                    Length = match.Length
                                });
        }

        public class FindInfo
        {
            public string Content { get; set; }

            public string Expression { get; set; }

            public int Index { get; set; }

            public int Length { get; set; }
        }
    }
}