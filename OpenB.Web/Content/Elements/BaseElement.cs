namespace OpenB.Web.Content.Elements
{
    public abstract class BaseElement : IElement
    {
        [AttributeName("key")]
        public string Key
        {
            get;
            set;
        }

        protected readonly RenderContext renderContext;
        protected BaseElement(RenderContext renderContext)
        {
            this.renderContext = renderContext;
        }
    }
}