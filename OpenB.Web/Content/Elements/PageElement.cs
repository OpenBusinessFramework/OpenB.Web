using System.Collections.Generic;

namespace OpenB.Web.Content.Elements
{
    [ElementName("text")]
    public class TextElement : BaseElement
    {
        public TextElement(RenderContext renderContext, IElementContainer parent) : base(renderContext, parent)
        {
            Paragraphs = new List<ParagraphElement>();
        }

        public IList<ParagraphElement> Paragraphs { get; set; }
    }

    [ElementName("page")]
    public class PageElement : BaseElement, IElementContainer
    {
        public IList<IElement> Elements
        {
            get;
            private set;
        }

        public PageElement(RenderContext renderContext, IElementContainer parent) : base(renderContext, parent)
        {
            Elements = new List<IElement>();
        }

        [AttributeName("title")]
        public string Title
        {
            get;
            set;
        }

        [AttributeName("model")]
        public object Model
        {
            get; set;
        }
    }
}