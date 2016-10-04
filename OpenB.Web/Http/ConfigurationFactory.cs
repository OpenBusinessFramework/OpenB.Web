using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace OpenB.Web.Http
{
    internal class ConfigurationFactory
    {
        internal static Configuration GetConfiguration()
        {
            string configurationFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "WebSolution.Config.xml");

            XmlDocument configurationDocument = new XmlDocument();
            configurationDocument.Load(configurationFilePath);

            

            Type configurationType = typeof(Configuration);
            Configuration configuration = new Configuration();

            foreach(PropertyInfo configurationProperty in configurationType.GetProperties(BindingFlags.Public))
            {
                var configurationNode = configurationDocument.SelectSingleNode("Configuration/" + configurationProperty.Name);
                if (configurationNode != null)
                {
                    configurationProperty.SetValue(configuration, configurationNode.Value);
                                    }
            }

            return configuration;
        }
    }
}