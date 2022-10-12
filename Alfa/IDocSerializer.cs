namespace Convertor;

interface IDocSerializer
{
    /// <summary>
    /// Converts Document object into text of different formats
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    string Serialize(Document document);
}
