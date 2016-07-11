using System;
using OpenB.Web.Content.Elements;
using OpenB.Web.Content.Templating;
using OpenB.Web.Content;
using System.Web.UI;

namespace OpenB.WebPackages.BootStrap.Templates
{
    public class TextboxTemplate : BaseWebControlTemplate<TextboxElement>, IWebControlTemplate<TextboxElement>
    {
        public TextboxTemplate(TextboxElement textbox, RenderContext renderContext): base (textbox, renderContext)
        {
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            if (Element.Value != null)
            {
                RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Value, Element.Value);
            }

            RenderContext.HtmlTextWriter.AddAttribute("ng-model", Element.AggregatedKey);
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Input);
            RenderContext.HtmlTextWriter.RenderEndTag();
        }
    }
}