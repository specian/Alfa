using System.Xml.Serialization;

namespace Convertor;

/// <summary>
/// Returns document as XML
/// </summary>
public class XmlDocSerializer : IDocSerializer
{
    public string Serialize(Document document)
    {
        using StringWriter sw = new();
        var s = new XmlSerializer(typeof(Document));
        s.Serialize(sw, document); ;
        return sw.ToString();
    }
}
