using Convertor;

namespace ConvertorTests
{
    public class ConvertorTests
    {
        private readonly ConvertService _service;

        private const string Xml1 = "<?xml version=\"1.0\"?><root><title>Titulek</title><text>Textový obsah</text></root>";
        private const string Xml2 = @"<?xml version=""1.0"" encoding=""utf-8""?>
            <root>
              <title>
                Test
              </title>
              <text>
                Lorem ipsum dolor sit amet.
              </text>
            </root>";

        private const string XmlOutput = @"<?xml version=""1.0"" encoding=""utf-16""?><Document xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><Title>Titulek</Title><Text>Textový obsah</Text></Document>";
        private const string Json1 = "{\"Title\":\"Titulek\",\"Text\":\"Textový obsah\"}";

        private readonly Document Doc1 = new()
        {
            Title = "Titulek",
            Text = "Textový obsah"
        };

        public ConvertorTests()
        {
            _service = new ConvertService();
        }

        [Fact]
        public void Convert_XmlToJson_1()
        {
            var json = _service.Convert("xml", "json", Xml1);
            Assert.Equal("{\"Title\":\"Titulek\",\"Text\":\"Textový obsah\"}", json);
        }

        [Fact]
        public void Convert_XmlToJson_2()
        {
            var json = _service.Convert("xml", "json", Xml2);
            Assert.Equal("{\"Title\":\"Test\",\"Text\":\"Lorem ipsum dolor sit amet.\"}", json);
        }

        [Fact]
        public void Convert_XmlToJson_EmptyInput()
        {
            var json = _service.Convert("xml", "json", "");
            Assert.Equal("Error: Root element is missing.", json);
        }

        [Fact]
        public void Convert_XmlToJson_BadXml()
        {
            var json = _service.Convert("xml", "json", "<?xml version=\"1.0\"?><title>Title</title><text>Text</text>");
            Assert.Equal("Error: There are multiple root elements. Line 1, position 43.", json);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("fuk", "buk", "")]
        public void Convert_BadFormatParams(string inputFormat, string outputFormat, string body)
        {
            var json = _service.Convert(inputFormat, outputFormat, body);
            Assert.Equal("Error: Input format not implemented", json);
        }

        [Fact]
        public void Convert_BadOutputFormatParam()
        {
            var json = _service.Convert("xml", "buk", "");
            Assert.Equal("Error: Output format not implemented", json);
        }

        [Fact]
        public void Input_Xml()
        {
            var parser = new XmlDocParser();
            var doc = parser.Parse(Xml1);
            InputAsserts(doc);
        }

        [Fact]
        public void Input_Json()
        {
            var parser = new JsonDocParser();
            var doc = parser.Parse(Json1);
            InputAsserts(doc);
        }

        private static void InputAsserts(Document? document)
        {
            Assert.NotNull(document);
            Assert.Equal("Titulek", document!.Title);
            Assert.Equal("Textový obsah", document.Text);
        }

        [Fact]
        public void Output_Xml()
        {
            var serializer = new XmlDocSerializer();
            var xml = serializer.Serialize(Doc1);
            Assert.Equal(XmlOutput, xml);
        }

        [Fact]
        public void Output_Json()
        {
            var serializer = new JsonDocSerializer();
            var json = serializer.Serialize(Doc1);
            Assert.Equal(Json1, json);
        }
    }
}