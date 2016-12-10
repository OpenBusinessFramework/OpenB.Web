using System;
using System.Linq;
using System.Reflection;

namespace OpenB.Web
{
    public class WebSolution
    {
        public string Name { get; private set; }
        public IWebPackage WebPackage { get; private set; }
        public WebSolutionConfiguration Configuration { get; private set; }
        public IUrlMapper UrlMapper { get; internal set; }

        readonly WebSolutionConfiguration configuration;

        public WebSolution(WebSolutionConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));            

            this.configuration = configuration;
            WebPackage = GetWebPackage(configuration.WebPackage);
            UrlMapper = GetUrlMapper(configuration.DefaultLandingPage);

            Name = configuration.Name;
        }

        private IUrlMapper GetUrlMapper(string defaultUrl)
        {
            return new UrlMapper(defaultUrl);
        }

        private IWebPackage GetWebPackage(string packageName)
        {
            Assembly packageAssembly = Assembly.Load(packageName);

            Type webPackageType = packageAssembly.GetExportedTypes().Where(t => typeof(IWebPackage).IsAssignableFrom(t)).SingleOrDefault();

            if (webPackageType != null)
            {
               return (IWebPackage)Activator.CreateInstance(webPackageType);
            }

            throw new NotSupportedException($"Cannot load webpackage with name {packageName}.");
        }
    }
}
