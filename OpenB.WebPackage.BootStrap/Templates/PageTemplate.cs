using System.Web.UI;
using OpenB.Web.Content.Elements;
using OpenB.Web.Content.Templating;
using OpenB.Web.Content;

namespace OpenB.WebPackages.BootStrap.Templates
{
    public class PageTemplate : BaseWebControlTemplate<PageElement>, IWebControlTemplate<PageElement>
    {
        public PageTemplate(PageElement page, RenderContext renderContext): base (page, renderContext)
        {
        }

        public override void Initialize()
        {
        }

        public override void Render()
        {
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Html);
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Head);
            RenderContext.HtmlTextWriter.RenderEndTag();
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Body);
            RenderContext.HtmlTextWriter.RenderEndTag();
            RenderContext.HtmlTextWriter.RenderEndTag();
        }
    }
}