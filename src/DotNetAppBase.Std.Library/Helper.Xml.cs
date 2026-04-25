using System.Xml.Linq;

namespace DotNetAppBase.Std.Library;

public partial class XHelper
{
    public static class Xmls
    {
        public static string RemoveDocumentDefinition(string xml)
        {
            var xdoc = XDocument.Parse(xml);
            xdoc.Declaration = null;

            return xdoc.ToString();
        }
    }
}