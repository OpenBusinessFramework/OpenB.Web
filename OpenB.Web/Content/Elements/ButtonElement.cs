namespace OpenB.Web.Content.Elements
{
    [ElementName("button")]
    public class ButtonElement : BaseElement, IElement
    {
        public ButtonElement(RenderContext renderContext): base (renderContext)
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
    }
}