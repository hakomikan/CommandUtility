using System;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;

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

    public static class StringExtensions
    {
        public static string ReplaceKeepingIndent(this String source, string target, string newString)
        {
            return Regex.Replace(source, $"^(.*)({target})",
                match => {
                    var headString = match.Groups[1].Value;
                    var indent = Regex.Replace(headString, "[^\\s]", " ");
                    var newStringWithIndent = newString.Replace("\n", $"\n{indent}");
                    return headString + newStringWithIndent;
                }, RegexOptions.Multiline);
        }
    }

}
