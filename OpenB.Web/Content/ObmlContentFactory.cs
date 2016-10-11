using OpenB.Web.Content.Elements;
using OpenB.Web.Http;
using OpenB.Web.View.Binding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

            BindValue(currentNode, nodeName, controlType, element);

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

        private static void BindValue(XmlNode currentNode, string nodeName, Type controlType, IElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            if (controlType == null)
                throw new ArgumentNullException(nameof(controlType));
            if (currentNode == null)
                throw new ArgumentNullException(nameof(currentNode));

            bool modelRetrieved = false;
            object model = null;

            foreach (XmlAttribute attribute in currentNode.Attributes)
            {
                // skip namespaced attributes, they belong to other implementations.
                if (attribute.Name.Contains(":"))
                    continue;

                PropertyInfo property = controlType.GetProperties().SingleOrDefault(p => p.GetCustomAttributes(typeof(AttributeNameAttribute), false).Cast<AttributeNameAttribute>().Any(c => c.Name.Equals(attribute.Name)));

                if (property != null)
                {
                    // Check if databinding is required.
                    Regex regularExpression = new Regex(@"\[(?<bindingExpression>.*)\]");
                    Match regularExprMatch = regularExpression.Match(attribute.Value);

                    if (regularExprMatch.Success)
                    {
                        model = BindModelData(currentNode, modelRetrieved, model, attribute, regularExprMatch);
                    }
                    else
                    {
                        model = attribute.Value;
                    }
                                        
                    if (model != null)
                    {
                        

                        var value = ValueConverterFactory.GetInstance().ConvertTo(property.PropertyType, model);
                        property.SetValue(element, value);
                    }
                       
                    
                   

                }
                else
                {
                    throw new AttributeNotSupportedException($"Control {nodeName} is does not support attribute {attribute.Name}.");
                }
            }
        }

        private static object BindModelData(XmlNode currentNode, bool modelRetrieved, object model, XmlAttribute attribute, Match regularExprMatch)
        {
            object currentModel = null;
            
            // Binding requested
            var matchGroup = regularExprMatch.Groups["bindingExpression"];
            if (matchGroup.Success)
            {
                model = RetrieveModel(currentNode, modelRetrieved, model, attribute);

                string[] requestedPropertyPath = matchGroup.Value.Split('.');

                // Cascading get properties.
                currentModel = model;

                foreach (var subProperty in requestedPropertyPath)
                {
                    if (currentModel != null)
                    {
                        PropertyInfo currentProperty = currentModel.GetType().GetProperty(subProperty);

                        if (currentProperty != null)
                        {
                            currentModel = currentProperty.GetValue(currentModel);
                        }
                        else
                        {
                            throw new Exception($"Cannot get the property {subProperty} for {requestedPropertyPath}.");
                        }
                    }
                    else
                    {
                        throw new Exception($"Cannot get the property {subProperty} for {requestedPropertyPath}.");
                    }
                }
            }

            return currentModel;
        }

        private static object RetrieveModel(XmlNode currentNode, bool modelRetrieved, object model, XmlAttribute attribute)
        {
            if (!modelRetrieved)
            {
                string modelReference = GetModelReference(currentNode, attribute);

                // TODO: Move to service.
                var currentPath = AppDomain.CurrentDomain.BaseDirectory;
                Assembly relatedAssembly = Assembly.LoadFrom(Path.Combine(currentPath, "bin", "ViewModels.dll"));

                if (relatedAssembly != null)
                {
                    string fullClassName = relatedAssembly.GetName().Name + "." + modelReference;

                    Type type = relatedAssembly.GetType(fullClassName);

                    if (type != null)
                    {
                        model = Activator.CreateInstance(type);
                    }

                }
            }

            return model;
        }

        private static string GetModelReference(XmlNode currentNode, XmlAttribute attribute)
        {
            XmlAttribute modelAttribute = currentNode.Attributes["model"];
            if (modelAttribute == null)
            {
                if (currentNode.ParentNode != null)
                {
                    return GetModelReference(currentNode.ParentNode, attribute);
                }

                throw new NotSupportedException($"No model defined on current node or no parent found for parsing property {attribute.Name}.");
            }
            string modelReference = modelAttribute.Value;
            return modelReference;
        }
    }

   
}
