using OpenB.Web.Content.Elements;
using OpenB.Web.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml;

namespace OpenB.Web.Content
{
    class ObmlContentFactory : IObmlContentFactory
    {
        private IWebControlTemplateBinder controlTemplateBinder;

        public ObmlContentFactory(IWebControlTemplateBinder controlTemplateBinder)
        {
            if (controlTemplateBinder == null)
                throw new ArgumentNullException(nameof(controlTemplateBinder));

            this.controlTemplateBinder = controlTemplateBinder;
        }

        public WebRequestOutput Create(XmlDocument xmlDocument)
        {
            if (xmlDocument == null)
                throw new ArgumentNullException(nameof(xmlDocument));

            StringBuilder stringBuilder = new StringBuilder();
            TextWriter textWriter = new StringWriter(stringBuilder);
            RenderContext renderContext = new RenderContext(new HtmlTextWriter(textWriter));

            WebRequestOutput output = new WebRequestOutput { ContentType = "text/html" };

            IEnumerable<Type> availableTypes = this.GetType().Assembly.GetTypes().Where(t => typeof(IElement).IsAssignableFrom(t) && t.IsAbstract == false && t.IsInterface == false);

            var currentNode = xmlDocument.SelectSingleNode("/*[1]");

            try
            {
                var element = CreateElement(renderContext, currentNode, availableTypes);
                output.Response = stringBuilder.ToString();              
            }
            catch(ControlNotSupportedExecption cne)
            {

                output.Error = new ControlNotSupportedError(cne.NodeName);
            }
            return output;

        }       

        private IElement CreateElement(RenderContext renderContext, XmlNode currentNode, IEnumerable<Type> availableTypes)
        {
            var nodeName = currentNode.LocalName;
            Type controlType = availableTypes.SingleOrDefault(a => a.GetCustomAttributes(typeof(ElementNameAttribute), false).Cast<ElementNameAttribute>().Any(c => c.ElementName.Equals(nodeName)));

            if (controlType == null)
            {
                throw new ControlNotSupportedException($"Control {nodeName} is not supported.");
            }

            IElement element = (IElement)Activator.CreateInstance(controlType, renderContext);
            IElementContainer container = element as IElementContainer;


            if (container != null)
            {
                var childNodes = currentNode.ChildNodes;

                foreach (XmlNode node in childNodes)
                {
                    container.Elements.Add(CreateElement(renderContext, node, availableTypes));
                }

                return container;
            }

            return element;
            
        }          
    }
}
