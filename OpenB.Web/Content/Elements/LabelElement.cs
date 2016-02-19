namespace OpenB.Web.Content.Elements
{
    [ElementName("label")]
    public class LabelElement : BaseElement, IElement, IInlineElement
    {
        public LabelElement(RenderContext renderContext) : base(renderContext)
        {
        }

        [AttributeName("value")]
        public string Value { get; set; }
    }
}
