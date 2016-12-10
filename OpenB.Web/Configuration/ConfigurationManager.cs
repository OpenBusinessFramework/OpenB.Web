using OpenB.Web.Http;
using System;
using System.IO;

namespace OpenB.Web.Configuration
{
    public class ConfigurationManager
    {
        public ConfigurationManager(string configurationRootPath)
        {
            if (string.IsNullOrWhiteSpace(configurationRootPath))
                throw new ArgumentNullException(nameof(configurationRootPath));

            DirectoryInfo rootPath = new DirectoryInfo(configurationRootPath);

            if (!rootPath.Exists)
            {
                throw new ConfigurationException($"Path {rootPath.FullName} does not exist. Cannot initialize configuration.");
            }

            ConfigurationFactory configurationFactory = ConfigurationFactory.GetInstance();
            
        }
    }
}
