using OpenB.Web.Content.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenB.Web.Content;
using System.Web.UI;
using OpenB.Web.Content.Elements;

namespace OpenB.WebPackages.BootStrap.Templates
{
    class DatepickerTemplate : BaseWebControlTemplate<DateBox>, IWebControlTemplate<DateBox>
    {
        public DatepickerTemplate(DateBox element, RenderContext renderContext) : base(element, renderContext)
        {
        }

        public override void Initialize()
        {
            
        }

        public override void Render()
        {
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Input);
            //RenderContext.HtmlTextWriter.WriteEncodedText(RenderContext.DataBindingService.GetDisplayValue(element, nameof(DateBox.Date));
        }
    }
}
