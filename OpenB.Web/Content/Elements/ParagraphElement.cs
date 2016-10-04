using System.Collections.Generic;

namespace OpenB.Web.Content.Elements
{
    public class ParagraphElement
    {
        public IList<TextElementElement> TextElements { get; }

        public ParagraphElement()
        {
            TextElements = new List<TextElementElement>();
        }
    }

    public class HeadingElement : ParagraphElement
    {
        [AttributeName("size")]
        public int Size { get; set; }
    }

    public class TextElementElement
    {
        [AttributeName("format")]
        public TextFormat Format { get; set; }

        [AttributeName("value", true)]
        public string Value { get; set; }
    }
}