using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenB.Web.Content.Elements
{
    [ElementName("wizard")]
    public class WizardElement : PageElement
    {
        public WizardElement(RenderContext renderContext, IElementContainer parent) : base(renderContext, parent)
        {
        }
    }
}
