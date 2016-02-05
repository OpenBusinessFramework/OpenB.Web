using System;
using OpenB.Web;
using OpenB.Web.Content.Templating;
using System.Linq;
using OpenB.Web.Content.Elements;
using OpenB.Web.Content;

namespace OpenB.WebPackages.BootStrap
{
    internal class BootstrapControlTemplateBinder : IWebControlTemplateBinder
    {  

        public IWebControlTemplate BindTemplate(IElement element, RenderContext renderContext)
        {
            Type type = element.GetType();

            var types = this.GetType().Assembly.GetTypes().Where(t => typeof(IWebControlTemplate).IsAssignableFrom(t));

            Type template = types.Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GenericTypeArguments.All(a => a.Equals(type)))).Single(t => typeof(IWebControlTemplate).IsAssignableFrom(t));

            var templateInstance = (IWebControlTemplate)Activator.CreateInstance(template, element, renderContext);

            var collectionTemplate = templateInstance as IWebControlCollectionTemplate;
            
            if (collectionTemplate != null)
            {
                var elementContainer = element as IElementContainer;
                foreach(IElement childElement in elementContainer.Elements)
                {
                    collectionTemplate.ChildTemplates.Add(BindTemplate(childElement, renderContext));
                }
            }

            return templateInstance;
        }
    }
}