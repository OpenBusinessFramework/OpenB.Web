using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace OpenB.Web.Http
{
    internal class ConfigurationFactory
    {
        private static ConfigurationFactory instance;
        private static ApplicationConfiguration applicationConfiguration;

        private string configurationFilePath;

        private ConfigurationFactory(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            configurationFilePath = filePath;
        }

        internal ApplicationConfiguration ApplicationConfiguration
        {
            get
            {
                if (applicationConfiguration == null)
                {
                 

                    XmlDocument configurationDocument = new XmlDocument();
                    configurationDocument.Load(configurationFilePath);

                    Type configurationType = typeof(ApplicationConfiguration);

                    XmlSerializer serializer = new XmlSerializer(typeof(ApplicationConfiguration));

                    StreamReader reader = new StreamReader(configurationFilePath);
                    applicationConfiguration = (ApplicationConfiguration)serializer.Deserialize(reader);
                    reader.Close();

                    applicationConfiguration.ConfigurationPath = new FileInfo(configurationFilePath).DirectoryName;
                }
                return applicationConfiguration;
            }
          
        }

        private void AssignValue(object currentObject, PropertyInfo relatedProperty, XmlNode xmlNode)
        {
            if (xmlNode == null)
                throw new ArgumentNullException(nameof(xmlNode));
            if (relatedProperty == null)
                throw new ArgumentNullException(nameof(relatedProperty));
          
            if (xmlNode.ChildNodes.Count > 0)
            {
                
            }
        }

        internal static ConfigurationFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new ConfigurationFactory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "WebSolution.Config.xml"));
               
            }

            return instance;
        }
    }
}