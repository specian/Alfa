namespace Convertor;

/// <summary>
/// Returns document in raw text format
/// </summary>
public class TextDocSerializer : IDocSerializer
{
    public string Serialize(Document document)
    {
        return $"Title: {document.Title}, Text: {document.Text}";
    }
}
