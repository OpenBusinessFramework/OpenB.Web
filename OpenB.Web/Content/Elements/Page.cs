using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenB.Web.Content.Elements
{
    public abstract class BaseElement : IElement
    {
        public string Key
        {
            get; set;
        }

        protected readonly RenderContext renderContext;

        protected BaseElement(RenderContext renderContext)
        {
            this.renderContext = renderContext;
        }
    }

    [ElementName("page")]
    public class PageElement : BaseElement, IElementContainer
    {
       

        public IList<IElement> Elements
        {
            get; private set;
           
        }

       

        public PageElement(RenderContext renderContext) : base(renderContext)
        {
           

            Elements = new List<IElement>();
        }
    }

    [ElementName("textbox")]
    public class TextboxElement : BaseElement, IElement
    {

        public TextboxElement(RenderContext renderContext) : base(renderContext)
        {

        }
    }

    public interface IElementContainer : IElement
    {
        IList<IElement> Elements { get; }
    }

    public interface IElement
    {
        string Key { get; }
    }

    internal class ElementNameAttribute : Attribute
    {
        public string ElementName { get; private set; }

        public ElementNameAttribute(string elementName)
        {
            this.ElementName = elementName;
        }
    }
}
