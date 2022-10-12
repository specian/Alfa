using Newtonsoft.Json;

namespace Convertor;

/// <summary>
/// Returns document as JSON
/// </summary>
public class JsonDocSerializer : IDocSerializer
{
    public string Serialize(Document document)
    {
        return JsonConvert.SerializeObject(document);
    }
}
