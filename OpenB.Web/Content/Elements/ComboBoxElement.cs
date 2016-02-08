using System.Collections.Generic;

namespace OpenB.Web.Content.Elements
{
    [ElementName("combobox")]
    public class ComboBoxElement : BaseElement, IElement
    {
        public ComboBoxElement(RenderContext renderContext): base (renderContext)
        {
            Items = new List<ComboboxItem>();

            Items.Add(new ComboboxTextItem() { Value = "1"});
            Items.Add(new ComboboxTextItem() { Value = "2" });
            Items.Add(new ComboboxTextItem() { Value = "3" });
        }

        public IList<ComboboxItem> Items { get; private set; }

        [AttributeName("name")]
        public string Name { get; set;  }
    }
}