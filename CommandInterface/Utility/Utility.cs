using System;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace CommandInterface.Utility
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }

    public static class Utility
    {
        public static string LoadAndWriteXml(string xmlText, Action<XDocument> processor = null)
        {
            using (var reader = new StringReader(xmlText))
            using (var writer = new Utf8StringWriter())
            {
                var xdoc = XDocument.Load(reader);
                xdoc.Declaration = new XDeclaration("1.0", "utf-8", null);

                if (processor != null)
                {
                    processor(xdoc);
                }

                xdoc.Save(writer);
                return writer.ToString();
            }
        }
    }

    public static class GuidExtensions
    {
        public static string BraceExpression(this Guid guid)
        {
            return $"{{{guid.ToString().ToUpper()}}}";
        }
    }

}
