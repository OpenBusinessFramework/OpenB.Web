using System.Collections.Generic;

namespace OpenB.Web.Content.Elements
{
    [ElementName("component")]
    public class ComponentElement : BaseElement, IElementContainer, IDataBoundElement
    {
        public ComponentElement(RenderContext renderContext, IElementContainer parent): base (renderContext, parent)
        {
            Elements = new List<IElement>();
        }

        [AttributeName("model")]
        public string Model { get; set; }

        public IList<IElement> Elements
        {
            get;
            private set;
        }

        [AttributeName("title")]
        public string Title { get; set; }     
    }
}
