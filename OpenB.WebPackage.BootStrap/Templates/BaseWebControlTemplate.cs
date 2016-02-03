using OpenB.Web.Content;

namespace OpenB.WebPackages.BootStrap.Templates
{
    public abstract class BaseWebControlTemplate<T>
    {
        protected T Element
        {
            get;
            private set;
        }

        protected RenderContext RenderContext
        {
            get;
            private set;
        }

        protected BaseWebControlTemplate(T element, RenderContext renderContext)
        {
            if (renderContext == null)
                throw new System.ArgumentNullException(nameof(renderContext));
            if (element == null)
                throw new System.ArgumentNullException(nameof(element));
            this.Element = element;
            this.RenderContext = renderContext;
        }

        public abstract void Initialize();
        public abstract void Render();
    }
}