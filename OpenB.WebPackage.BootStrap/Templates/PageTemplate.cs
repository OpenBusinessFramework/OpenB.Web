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

            // <html>
           RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Html);

            // <html><head>
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Head);

            if (!string.IsNullOrEmpty(Element.Title))
            {
                // <html><head><title>
                RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Title);
                RenderContext.HtmlTextWriter.Write(Element.Title);
                RenderContext.HtmlTextWriter.RenderEndTag();
                // <html><head></title>
            }

            //  <html><head><meta>
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Name, "viewport");
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Content, "width=device-width, initial-scale=1");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Meta);

            var completeApplicationUrl = string.Format($"{RenderContext.RequestUri.Authority}{RenderContext.ApplicationPath}");

            foreach (var referenceLink in RenderContext.ReferenceService.GetLinks(RenderContext.RequestUri.Scheme, completeApplicationUrl))
            {               
                RenderContext.HtmlTextWriter.Write(referenceLink);
            }

            RenderContext.HtmlTextWriter.RenderEndTag();
            RenderContext.HtmlTextWriter.RenderEndTag();

            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Body);
            RenderContext.HtmlTextWriter.AddAttribute("ng-controller", string.Concat(Element.Key, "Controller"));
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