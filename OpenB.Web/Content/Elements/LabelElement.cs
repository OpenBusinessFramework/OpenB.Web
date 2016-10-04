namespace OpenB.Web.Content.Elements
{
    [ElementName("label")]
    public class LabelElement : BaseElement, IElement, IInlineElement
    {
        public LabelElement(RenderContext renderContext, IElementContainer parent) : base(renderContext, parent)
        {
        }

        [AttributeName("value")]
        public string Value { get; set; }
    }
}
