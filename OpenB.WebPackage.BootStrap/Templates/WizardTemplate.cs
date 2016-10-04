using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenB.Web.Content;
using OpenB.Web.Content.Elements;
using System.Web.UI;

namespace OpenB.WebPackages.BootStrap.Templates
{
    public class WizardTemplate : BaseWebControlTemplate<WizardElement>
    {
        public WizardTemplate(WizardElement wizard, RenderContext renderContext) : base(wizard, renderContext)
        {
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
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
            RenderContext.HtmlTextWriter.AddAttribute("ng-controller", string.Concat(Element.AggregatedKey, "Controller"));
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "container");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Div);

         

            RenderContext.HtmlTextWriter.RenderEndTag();

            RenderContext.HtmlTextWriter.RenderEndTag();
            RenderContext.HtmlTextWriter.RenderEndTag();
        }
    }
    }

