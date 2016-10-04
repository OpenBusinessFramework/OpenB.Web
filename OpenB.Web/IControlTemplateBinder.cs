using OpenB.Web.Content;
using OpenB.Web.Content.Elements;
using OpenB.Web.Content.Templating;
using System;

namespace OpenB.Web
{
    public interface IWebControlTemplateBinder
    {        
        IWebControlTemplate BindTemplate(IElement type, RenderContext renderContext);
    }
}