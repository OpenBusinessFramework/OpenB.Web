namespace OpenB.Web.Content.Elements
{
    [ElementName("textbox")]
    public class TextboxElement : BaseElement, IElement
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

    [ElementName("checkbox")]
    public class CheckboxElement : BaseElement, IElement
    {
        public CheckboxElement(RenderContext renderContext, IElementContainer parent) : base(renderContext, parent)
        {
        }

        [AttributeName("value")]
        public bool Value
        {
            get; set;
        }      
    }
}