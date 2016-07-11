using System;
using OpenB.Web.Content.Elements;
using OpenB.Web.Content.Templating;
using OpenB.Web.Content;
using System.Web.UI;

namespace OpenB.WebPackages.BootStrap.Templates
{
    public class ButtonTemplate : BaseWebControlTemplate<ButtonElement>, IWebControlTemplate<ButtonElement>
    {
        public ButtonTemplate(ButtonElement button, RenderContext renderContext): base (button, renderContext)
        {
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Type, "btn");

            if (!Element.Enabled)
            {
                RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Type, "btn");
            }

            if (!string.IsNullOrEmpty(Element.Action))
            {
                RenderContext.HtmlTextWriter.AddAttribute("ng-click", "handle()");
            }

            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Button);
            RenderContext.HtmlTextWriter.Write(Element.Value);
            RenderContext.HtmlTextWriter.RenderEndTag();
        }
    }
}