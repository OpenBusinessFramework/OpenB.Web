using System;

namespace OpenB.Web
{
    public class WebSolution
    {
        public string Name { get; private set; }
        public IWebPackage WebPackage { get; private set; }

        public WebSolution(string name, IWebPackage webPackage)
        {
            if (webPackage == null)
            {
                throw new ArgumentNullException(nameof(webPackage));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new NotSupportedException("A WebSolution should have a name.");
            }

            WebPackage = webPackage;
            Name = name;
        }        
    }
}
