using System;
using OpenB.Web.Content.Elements;
using OpenB.Web.Content.Templating;
using OpenB.Web.Content;
using System.Web.UI;

namespace OpenB.WebPackages.BootStrap.Templates
{
    public class ComboboxTemplate : BaseWebControlTemplate<ComboBoxElement>, IWebControlTemplate<ComboBoxElement>
    {
        public ComboboxTemplate(ComboBoxElement combobox, RenderContext renderContext) : base(combobox, renderContext)
        {
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-control");
            RenderContext.HtmlTextWriter.AddAttribute("ng-model", Element.AggregatedKey);
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Select);            

           
             
            foreach(ComboboxItem item in Element.Items)
            {
                RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Option);               

                if (item is ComboboxTextItem)
                {
                    
                    RenderContext.HtmlTextWriter.Write((item as ComboboxTextItem).Value);                   
                }
               
                RenderContext.HtmlTextWriter.RenderEndTag();
            }


            RenderContext.HtmlTextWriter.RenderEndTag();
            
        }
    }
}