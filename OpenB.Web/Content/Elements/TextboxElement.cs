namespace OpenB.Web.Content.Elements
{
    [ElementName("textbox")]
    public class TextboxElement : BaseInPageElement, IElement
    {
        public TextboxElement(RenderContext renderContext, IElementContainer parent): base (renderContext, parent)
        {
        }

        [AttributeName("value")]
        public string Value
        {
            get; set;
        }

        [AttributeName("model")]
        public string Model { get; set; }
    }
}