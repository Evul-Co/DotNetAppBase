using System.Xml.Linq;
using System.Linq;
using System.Xml;

// ReSharper disable CheckNamespace
public static class XXmlDocumentExtensions
    // ReSharper restore CheckNamespace
{
    public static XmlAttribute AttributeCreate(this XmlDocument document, string prefix, string name, string value)
    {
        var attribute = document.CreateAttribute(prefix, name, "http://www.w3.org/2000/xmlns/");
        attribute.Value = value;

        return attribute;
    }

    public static XmlDocument RemoveXmlns(this XmlDocument doc)
    {
        XDocument d;
        using (var nodeReader = new XmlNodeReader(doc))
        {
            d = XDocument.Load(nodeReader);
        }

        d.Root.Descendants().Attributes().Where(x => x.IsNamespaceDeclaration).Remove();

        foreach (var elem in d.Descendants())
        {
            elem.Name = elem.Name.LocalName;
        }

        var xmlDocument = new XmlDocument();
        using (var xmlReader = d.CreateReader())
        {
            xmlDocument.Load(xmlReader);
        }

        return xmlDocument;
    }
}