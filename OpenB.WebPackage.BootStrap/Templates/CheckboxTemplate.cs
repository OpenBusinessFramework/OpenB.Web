using System;
using OpenB.Web.Content.Elements;
using OpenB.Web.Content.Templating;
using OpenB.Web.Content;
using System.Web.UI;

namespace OpenB.WebPackages.BootStrap.Templates
{
    public class CheckboxTemplate : BaseWebControlTemplate<CheckboxElement>, IWebControlTemplate<CheckboxElement>
    {
        public CheckboxTemplate(CheckboxElement textbox, RenderContext renderContext): base (textbox, renderContext)
        {
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            if (Element.Value)
            {
                RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
            }

            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Input);
            RenderContext.HtmlTextWriter.RenderEndTag();
        }
    }
}