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
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Div);


            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-default dropdown-toggle");
            RenderContext.HtmlTextWriter.AddAttribute("data-toggle", "dropdown");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Button);
            RenderContext.HtmlTextWriter.Write(Element.Name);

            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "caret");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Span);

            RenderContext.HtmlTextWriter.RenderEndTag();
            RenderContext.HtmlTextWriter.RenderEndTag();

            RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown-menu");
            RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Ul);
             
            foreach(ComboboxItem item in Element.Items)
            {
                RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Li);               

                if (item is ComboboxTextItem)
                {
                    RenderContext.HtmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Href , "#");
                    RenderContext.HtmlTextWriter.RenderBeginTag(HtmlTextWriterTag.A);
                    RenderContext.HtmlTextWriter.Write((item as ComboboxTextItem).Value);
                    RenderContext.HtmlTextWriter.RenderEndTag();
                }
               
                RenderContext.HtmlTextWriter.RenderEndTag();
            }


            RenderContext.HtmlTextWriter.RenderEndTag();
            RenderContext.HtmlTextWriter.RenderEndTag();
        }
    }
}