using OpenB.Web.Content.Elements;
using System;
using OpenB.Web.Content;
using System.Web.UI;
using OpenB.Web.Content.Templating;

namespace OpenB.WebPackages.BootStrap.Templates
{
    public class TextElementTemplate : BaseWebControlTemplate<TextElement>, IWebControlTemplate<TextElement>
    {
        public TextElementTemplate(TextElement element, RenderContext renderContext) : base(element, renderContext)
        {
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "text");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Div);

            foreach(ParagraphElement paragraph in Element.Paragraphs)
            {

            }

            RenderContext.HtmlTextWriter.RenderEndTag();         
        }
    }
}
