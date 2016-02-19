using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenB.Web.Content.Elements
{
    [ElementName("component")]
    public class ComponentElement : BaseElement, IElementContainer, IDataBoundElement
    {
        public ComponentElement(RenderContext renderContext): base (renderContext)
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
