using System;
using System.Collections.Generic;
using System.Xml;

namespace OpenB.Web.Test.View
{
    public class MappingConfiguration
    {
        private string mappingFilePath;

        public MappingConfiguration(string mappingFile)
        {
            if (mappingFile == null)
                throw new ArgumentNullException(nameof(mappingFile));

            mappingFilePath = mappingFile;

            Parse();
        }

        public string Model { get; private set; }
        public IList<PropertyMapping> Properties { get; private set; }

        private void Parse()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(mappingFilePath);

            XmlNamespaceManager nameSpaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            nameSpaceManager.AddNamespace("map", "http://www.openb.org/mapping/viewmodel");

            XmlNode rootNode = xmlDocument.SelectSingleNode("/map:ViewModelMapping", nameSpaceManager);

            if (rootNode != null)
            {
                XmlAttribute modelNameAttribute = rootNode.Attributes["model"];
                if (modelNameAttribute != null)
                {
                    Model = modelNameAttribute.Value;
                }
            }

            XmlNodeList propertyNodes = rootNode.SelectNodes("map:property", nameSpaceManager);
            this.Properties = new List<PropertyMapping>();

            ParseProperties(propertyNodes);
        }

        private void ParseProperties(XmlNodeList propertyNodes)
        {
            if (propertyNodes == null)
                throw new ArgumentNullException(nameof(propertyNodes));

            foreach (XmlNode propertyNode in propertyNodes)
            {
                XmlAttribute nameAttribute = propertyNode.Attributes["name"];
                if (nameAttribute != null)
                {
                    PropertyMapping propertyMapping = new PropertyMapping(nameAttribute.Value);

                    this.Properties.Add(propertyMapping);
                }


            }
        }
    }
}