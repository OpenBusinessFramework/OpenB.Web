using OpenB.Web;
using System.Collections.Generic;

namespace OpenB.WebPackages.BootStrap
{
    public class BootStrapWebPackage : BaseWebPackage, IWebPackage
    {
        public BootStrapWebPackage(): base (new BootstrapControlTemplateBinder(), new BootStrapWebRequestFactory(new List<IWebReference>() { new JavascriptReference("bootstrap.min.js"), new JavascriptReference("angular.min.js"), new CascadingStyleSheetReference("bootstrap-theme.min.css"), new CascadingStyleSheetReference("bootstrap.min.css") }))
        {
        }

       
    }
}