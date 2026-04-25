using DotNetAppBase.Std.Library;

// ReSharper disable CheckNamespace
public static partial class XObjectExtensions
// ReSharper restore CheckNamespace
{
    public static string AsJson(this object obj) => Equals(obj, null) ? NullDefaultDisplay : XHelper.Serializers.Json.Serialize(obj);

    public static string AsXml(this object obj) => Equals(obj, null) ? NullDefaultDisplay : XHelper.Serializers.Xml.Serialize(obj);

    public static string AsXmlDataContract(this object obj) => Equals(obj, null) ? NullDefaultDisplay : XHelper.Serializers.Xml.SerializeDataContract(obj);
}