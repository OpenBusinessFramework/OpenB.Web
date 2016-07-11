using OpenB.Web.Content.Elements;
using OpenB.Web.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Xml;

namespace OpenB.Web.Content
{
    class ObmlContentFactory : IObmlContentFactory
    {
        readonly IWebControlTemplateBinder controlTemplateBinder;
        readonly IWebReferenceService referenceService;

        public ObmlContentFactory(IWebControlTemplateBinder controlTemplateBinder, IWebReferenceService referenceService)
        {
            if (referenceService == null)
                throw new ArgumentNullException(nameof(referenceService));
            
            if (controlTemplateBinder == null)
                throw new ArgumentNullException(nameof(controlTemplateBinder));

            this.referenceService = referenceService;
            this.controlTemplateBinder = controlTemplateBinder;
        }

        public WebRequestOutput Create(string applicationPath, Uri uri, XmlDocument xmlDocument)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (xmlDocument == null)
                throw new ArgumentNullException(nameof(xmlDocument));

            string applicationHost = GetApplicationHost(uri);

            StringBuilder stringBuilder = new StringBuilder();
            TextWriter textWriter = new StringWriter(stringBuilder);
            RenderContext renderContext = new RenderContext(new HtmlTextWriter(textWriter), referenceService, uri, applicationPath, applicationHost);

            WebRequestOutput output = new WebRequestOutput { ContentType = "text/html" };

            IEnumerable<Type> availableTypes = this.GetType().Assembly.GetTypes().Where(t => typeof(IElement).IsAssignableFrom(t) && t.IsAbstract == false && t.IsInterface == false);

            var currentNode = xmlDocument.SelectSingleNode("/*[1]");

            try
            {
                var element = CreateElement(renderContext, currentNode, availableTypes, null);
                var template = controlTemplateBinder.BindTemplate(element, renderContext);

                // TODO: Initialization to add scripts.
                template.Render();                

                output.Response = stringBuilder.ToString();
               

                renderContext.HtmlTextWriter.Close();
            }
            catch(ControlNotSupportedExecption cne)
            {
                output.Error = new ControlNotSupportedError(cne.NodeName);
            }
            return output;
        }

        private string GetApplicationHost(Uri uri)
        {
            bool isDefaultHttp = (uri.Port == 80 && uri.Scheme == "http");
            bool isDefaultHttps = (uri.Port == 443 && uri.Scheme == "https");

            if (isDefaultHttp || isDefaultHttps)
            {
                return $"{uri.Scheme}://{uri.Host}";
            }
            else
            { 
                return $"{uri.Scheme}://{uri.Host}:{uri.Port}";
            }
        }

        private IElement CreateElement(RenderContext renderContext, XmlNode currentNode, IEnumerable<Type> availableTypes, IElementContainer parent)
        {
            var nodeName = currentNode.LocalName;
            Type controlType = availableTypes.SingleOrDefault(a => a.GetCustomAttributes(typeof(ElementNameAttribute), false).Cast<ElementNameAttribute>().Any(c => c.ElementName.Equals(nodeName)));

            if (controlType == null)
            {
                throw new ControlNotSupportedException($"Control {nodeName} is not supported.");
            }

            IElement element = (IElement)Activator.CreateInstance(controlType, renderContext, parent);
                     

            foreach(XmlAttribute attribute in currentNode.Attributes)
            {
                // skip namespaced attributes, they belong to other implementations.
                if (attribute.Name.Contains(":"))
                    continue;

               PropertyInfo property = controlType.GetProperties().SingleOrDefault(p => p.GetCustomAttributes(typeof(AttributeNameAttribute), false).Cast<AttributeNameAttribute>().Any(c => c.Name.Equals(attribute.Name)));

                if (property != null)
                {
                    if (property.PropertyType == typeof(Boolean))
                    {
                        property.SetValue(element, Boolean.Parse(attribute.Value));
                    }
                    else
                    {
                        property.SetValue(element, attribute.Value);
                    }
                    
                }
                else
                {
                    throw new AttributeNotSupportedException($"Control {nodeName} is does not support attribute {attribute.Name}.");
                }
            }

            // Databinding.
            IDataBoundElement dataBoundElement = element as IDataBoundElement;
         

            if (dataBoundElement != null && dataBoundElement.Model != null)
            {
                // TODO: Move to service.
                var currentPath = AppDomain.CurrentDomain.BaseDirectory;
                Assembly relatedAssembly = Assembly.LoadFile(Path.Combine(currentPath, "Modules", "ViewModels.dll"));

                if (relatedAssembly != null)
                {                    
                    string fullClassName = relatedAssembly.GetName().Name + "." + dataBoundElement.Model;

                    if (parent != null)
                    {
                        IDataBoundElement dataBoundParent = parent as IDataBoundElement;

                        if (dataBoundParent != null && !string.IsNullOrEmpty(dataBoundParent.Model))
                        {

                            object relatedModel = renderContext.ModelCache[parent.AggregatedKey];
                            object model = relatedModel.GetType().GetProperty(dataBoundElement.Model).GetValue(relatedModel);

                            var comboBoxElement = element as ComboBoxElement;
                            if (comboBoxElement != null)
                            {
                                object dataSource = relatedModel.GetType().GetProperty(comboBoxElement.DataSource).GetValue(relatedModel);

                                foreach (var value in (dataSource as IEnumerable))
                                {
                                    comboBoxElement.Items.Add(new ComboboxTextItem() { Value = value.ToString()  });
                                }
                               
                            }
                        }
                        else
                        {

                            Type type = relatedAssembly.GetType(fullClassName);

                            if (type != null)
                            {
                                object model = Activator.CreateInstance(type);

                                if (model != null)
                                {
                                    renderContext.ModelCache.Add(element.AggregatedKey, model);
                                }
                            }
                            else
                            {
                                throw new NotSupportedException("Cannot load model");
                            }
                        }
                    }

                  
                }
            }

            IElementContainer container = element as IElementContainer;

            if (container != null)
            {
                var childNodes = currentNode.ChildNodes;

                foreach (XmlNode node in childNodes)
                {
                    container.Elements.Add(CreateElement(renderContext, node, availableTypes, container));
                }

                return container;
            }

            return element;
            
        }          
    }
}
