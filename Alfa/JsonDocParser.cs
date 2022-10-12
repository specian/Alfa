using Newtonsoft.Json;

namespace Convertor;

/// <summary>
/// Parses document from JSON
/// </summary>
public class JsonDocParser : IDocParser
{
    public Document? Parse(string input)
    {
        Document? doc = JsonConvert.DeserializeObject<Document>(input);
        return doc;
    }
}
