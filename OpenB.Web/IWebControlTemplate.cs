using OpenB.Web.Content.Elements;

namespace OpenB.Web.Content.Templating
{
    public interface IWebControlTemplate
    {
        void Render();
        void Initialize();
    }

    public interface IWebControlTemplate<T> : IWebControlTemplate where T : IElement
    {
     
    }    
}