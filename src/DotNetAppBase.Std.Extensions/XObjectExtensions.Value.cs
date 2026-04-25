using System.Xml.Serialization;
using DotNetAppBase.Std.Library;

// ReSharper disable CheckNamespace
public static partial class XObjectExtensions
// ReSharper restore CheckNamespace
{
    public static object NotNull(this object obj)
    {
        if (Equals(obj, null))
        {
            return null;
        }

        var type = obj.GetType();
        if (!type.IsEnum)
        {
            return obj.ToString();
        }

        var memberInfo = XHelper.Reflections.Fields.GetStatic(type, obj.ToString());
        var xmlEnumMember = XHelper.Reflections.Attributes.Get<XmlEnumAttribute>(memberInfo);

        return xmlEnumMember != null ? xmlEnumMember.Name : obj.ToString();
    }
}