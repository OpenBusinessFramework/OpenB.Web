using System.Collections.Generic;

namespace OpenB.Web.Content.Elements
{
    [ElementName("page")]
    public class PageElement : BaseElement, IElementContainer
    {
        public IList<IElement> Elements
        {
            get;
            private set;
        }

        public PageElement(RenderContext renderContext): base (renderContext)
        {
            Elements = new List<IElement>();
        }

        [AttributeName("title")]
        public string Title
        {
            get;
            set;
        }
    }
}