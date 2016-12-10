using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenB.Web.Content.Elements
{
    [ElementName("datebox")]
    public class DateBox : BaseInPageElement, IElement 
    {
        public DateBox(RenderContext renderContext, IElementContainer parent) : base(renderContext, parent)
        {
        }

        [AttributeName("value")]
        public DateTime Date { get; set; }
    }
}
