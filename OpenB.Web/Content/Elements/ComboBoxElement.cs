using System.Collections.Generic;

namespace OpenB.Web.Content.Elements
{
    [ElementName("combobox")]
    public class ComboBoxElement : BaseElement, IElement, IDataBoundElement
    {
        public ComboBoxElement(RenderContext renderContext, IElementContainer parent): base (renderContext, parent)
        {
            Items = new List<ComboboxItem>();       
        }

        public IList<ComboboxItem> Items { get; private set; }

        [AttributeName("name")]
        public string Name { get; set;  }

        [AttributeName("model")]
        public string Model { get; set; }

        [AttributeName("dataSource")]
        public string DataSource { get; internal set; }
    }
}