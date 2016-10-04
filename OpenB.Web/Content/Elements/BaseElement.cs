namespace OpenB.Web.Content.Elements
{
    public abstract class BaseInPageElement : BaseElement
    {
        public BaseInPageElement(RenderContext renderContext, IElementContainer parent) : base(renderContext, parent)
        {
        }

        [AttributeName("Visible")]
        public string Visible { get; set; }
    }

    public abstract class BaseElement : IElement
    {
        [AttributeName("key")]
        public string Key { get; set; }       
      
        public string AggregatedKey
        {
            get
            {
                if(Parent == null || Parent is PageElement || string.IsNullOrEmpty(Parent.AggregatedKey))
                {
                    return Key;
                }
                else
                {
                    return $"{Parent.AggregatedKey}.{Key}";
                }
            }          
        }

        public IElementContainer Parent { get; private set; }

        protected readonly RenderContext renderContext;      

        protected BaseElement(RenderContext renderContext, IElementContainer parent)
        {
            this.renderContext = renderContext;
            this.Parent = parent;
        }
    }
}