namespace Convertor;

/// <summary>
/// Parses document from XML
/// </summary>
public class XmlDocParser : IDocParser
{
    public Document Parse(string input)
    {
        var xdoc = System.Xml.Linq.XDocument.Parse(input);

        return new Document()
        {
            Title = xdoc.Root?.Element("title")?.Value.Trim(),
            Text = xdoc.Root?.Element("text")?.Value.Trim()
        };
    }
}
