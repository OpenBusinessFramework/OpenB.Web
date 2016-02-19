using System;
using System.Collections.Generic;
using OpenB.Web.Content.Elements;
using OpenB.Web.Content.Templating;
using OpenB.Web.Content;
using System.Web.UI;

namespace OpenB.WebPackages.BootStrap.Templates
{
    public class ComponentTemplate : BaseWebControlTemplate<ComponentElement>, IWebControlCollectionTemplate<ComponentElement>, IAngularController
    {
        public IList<IWebControlTemplate> ChildTemplates { get; private set; }

        public ComponentTemplate(ComponentElement component, RenderContext renderContext): base (component, renderContext)
        {
            ChildTemplates = new List<IWebControlTemplate>();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            // div
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "panel panel-default ng-scope");
            RenderContext.HtmlTextWriter.AddAttribute("data-host", RenderContext.RequestUri.Authority);
            RenderContext.HtmlTextWriter.AddAttribute("ng-app", "OpenB");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Div);

            if (Element.Title != null)
            {
                RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "panel-heading");
                RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Div);

                RenderContext.HtmlTextWriter.WriteEncodedText(Element.Title);

                RenderContext.HtmlTextWriter.RenderEndTag();

            }

            // div-div
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "panel-body");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Div);



            foreach (var child in ChildTemplates)
            {
                child.Render();
            }

            // end div-div
            RenderContext.HtmlTextWriter.RenderEndTag();

            // end div
            RenderContext.HtmlTextWriter.RenderEndTag();
        }
    }
}
