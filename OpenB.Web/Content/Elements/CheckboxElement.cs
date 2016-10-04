namespace OpenB.Web.Content.Elements
{

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