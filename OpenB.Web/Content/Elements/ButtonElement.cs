namespace OpenB.Web.Content.Elements
{
    [ElementName("button")]
    public class ButtonElement : BaseElement, IElement
    {
        public ButtonElement(RenderContext renderContext, IElementContainer parent): base (renderContext, parent)
        {
        }

        [AttributeName("Enabled")]
        public bool Enabled
        {
            get;
            set;
        }

        [AttributeName("value")]
        public string Value
        {
            get;
            set;
        }

        [AttributeName("action")]
        public string Action
        {
            get;
            set;
        }
    }
}