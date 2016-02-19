namespace OpenB.Web.Content.Elements
{
    [ElementName("textbox")]
    public class TextboxElement : BaseElement, IElement
    {
        public TextboxElement(RenderContext renderContext): base (renderContext)
        {
        }

        [AttributeName("value")]
        public string Value
        {
            get; set;
        }
    }

    [ElementName("checkbox")]
    public class CheckboxElement : BaseElement, IElement
    {
        public CheckboxElement(RenderContext renderContext) : base(renderContext)
        {
        }

        [AttributeName("value")]
        public bool Value
        {
            get; set;
        }
    }
}