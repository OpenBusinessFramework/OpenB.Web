using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace OpenB.Web
{
    public class WebSolutionFactory
    {
        const string mainConfiguration = "WebSolution.Config.xml";

        public static WebSolution CreateSolution()
        {
            var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var configurationFilePath = Path.Combine(baseDirectory, "Configuration", mainConfiguration);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(configurationFilePath);

            var modulesFolder = Path.Combine(baseDirectory, "Modules");

            DirectoryInfo directoryInfo = new DirectoryInfo(modulesFolder);

            IList<Assembly> assemblies = new List<Assembly>();
            foreach(FileInfo fileInfo in directoryInfo.GetFiles("*.dll"))
            { 
                assemblies.Add(Assembly.LoadFile(fileInfo.FullName));
            }

            var packageAssemblyRef =  xmlDocument.SelectSingleNode("/Configuration/WebSolution/WebPackage").InnerText;
            var solutionName = xmlDocument.SelectSingleNode("/Configuration/WebSolution/Name").InnerText;

            Assembly packageAssembly = Assembly.Load(packageAssemblyRef);

            Type webPackageType = packageAssembly.GetExportedTypes().Where(t => typeof(IWebPackage).IsAssignableFrom(t)).SingleOrDefault();

            if (webPackageType != null)
            {
               IWebPackage webPackage = (IWebPackage)  Activator.CreateInstance(webPackageType);
                WebSolution webSolution = new WebSolution(solutionName,webPackage);

                return webSolution;
            }       


            throw new NotSupportedException("No webpackages found for websolution.");

        }
    }
}
