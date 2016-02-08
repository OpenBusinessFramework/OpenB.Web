using System.Web.UI;
using OpenB.Web.Content.Elements;
using OpenB.Web.Content.Templating;
using OpenB.Web.Content;
using System.Collections.Generic;

namespace OpenB.WebPackages.BootStrap.Templates
{  

    public class PageTemplate : BaseWebControlTemplate<PageElement>, IWebControlCollectionTemplate<PageElement>, IAngularController
    {
        public IList<IWebControlTemplate> ChildTemplates { get; private set; }

        public PageTemplate(PageElement page, RenderContext renderContext): base (page, renderContext)
        {
            ChildTemplates = new List<IWebControlTemplate>();
        }

        public override void Initialize()
        {
        }

        public override void Render()
        {
            RenderContext.HtmlTextWriter.Write("<!DOCTYPE html>");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Html);
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Head);

            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Name, "viewport");
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Content, "width=device-width, initial-scale=1");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Meta);
            
            foreach (var referenceLink in RenderContext.ReferenceService.GetLinks(RenderContext.RequestUri.Scheme, RenderContext.RequestUri.Authority))
            {               
                RenderContext.HtmlTextWriter.Write(referenceLink);
            }

            RenderContext.HtmlTextWriter.RenderEndTag();
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Body);
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "container");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Div);
            
            foreach(var child in ChildTemplates)
            {
                child.Render();
            }

            RenderContext.HtmlTextWriter.RenderEndTag();
            RenderContext.HtmlTextWriter.RenderEndTag();
            RenderContext.HtmlTextWriter.RenderEndTag();
        }
    }
}