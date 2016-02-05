using OpenB.Web.Content.Elements;
using System.Collections.Generic;

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

    public interface IWebControlCollectionTemplate: IWebControlTemplate
    {
        IList<IWebControlTemplate> ChildTemplates { get; }
    }

    public interface IWebControlCollectionTemplate<T> : IWebControlCollectionTemplate where T : IElement
    {

    }
}