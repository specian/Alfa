namespace Convertor;

public class ConvertService
{
    /// <summary>
    /// Converts text in a given format to an other format
    /// </summary>
    /// <param name="inputFormat">Format of the input text</param>
    /// <param name="outputFormat">Format of the output text</param>
    /// <param name="body">Text to format</param>
    /// <returns>Output text</returns>
    /// <exception cref="ArgumentException"></exception>
    public string Convert(string inputFormat, string outputFormat, string body)
    {
        IDocParser parser;
        IDocSerializer serializer;

        try
        {
            parser = inputFormat switch
            {
                Constants.DocXml => new XmlDocParser(),
                Constants.DocJson => new JsonDocParser(),
                _ => throw new ArgumentException("Input format not implemented")
            };

            serializer = outputFormat switch
            {
                Constants.DocXml => new XmlDocSerializer(),
                Constants.DocJson => new JsonDocSerializer(),
                _ => throw new ArgumentException("Output format not implemented")
            };

            Document? doc = parser.Parse(body);
            if (doc is null || doc.Title is null || doc.Text is null)
            {
                return $"Error parsing input file";
            }

            var output = serializer.Serialize(doc);
            return output;
        }
        catch (Exception e)
        {
            return $"Error: {e.Message}";
        }
    }
}
