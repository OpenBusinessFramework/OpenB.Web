using System;
using System.Xml;

namespace OpenB.Web
{
    public class WebSolutionConfiguration 
    {
        public string Name { get;  set; }
        public string WebPackage { get;  set; }
        public string DefaultDomainModelNamespace { get;  set; }
        public string DefaultLandingPage { get;  set; }

        internal static WebSolutionConfiguration FromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
                throw new ArgumentNullException(nameof(xmlNode));

            var webPackage = xmlNode.SelectSingleNode("WebPackage").InnerText;
            var name  = xmlNode.SelectSingleNode("Name").InnerText;

            return new WebSolutionConfiguration()
            {
                Name = name,
                WebPackage = webPackage,
                DefaultLandingPage = "/MainPage.obml"
            };
        }
    }
}