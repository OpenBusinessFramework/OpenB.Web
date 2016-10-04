using System;
using OpenB.Web.Content.Elements;
using OpenB.Web.Content.Templating;
using OpenB.Web.Content;
using System.Web.UI;

namespace OpenB.WebPackages.BootStrap.Templates
{
    public class LabelTemplate : BaseWebControlTemplate<LabelElement>, IWebControlTemplate<LabelElement>
    {
        public LabelTemplate(LabelElement labelElement, RenderContext renderContext): base (labelElement, renderContext)
        {
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "label label-default");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Span);
            RenderContext.HtmlTextWriter.WriteEncodedText(Element.Value);
            RenderContext.HtmlTextWriter.RenderEndTag();
        }
    }
}