namespace Convertor;

interface IDocParser
{
    /// <summary>
    /// Parses input into Document
    /// </summary>
    /// <param name="input">Text input in XML, JSON or other format</param>
    /// <returns></returns>
    Document? Parse(string input);
}
