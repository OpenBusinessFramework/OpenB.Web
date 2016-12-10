using OpenB.Web;
using System.Collections.Generic;

namespace OpenB.WebPackages.BootStrap
{
    public class BootStrapWebPackage : BaseWebPackage, IWebPackage
    {
        public BootStrapWebPackage(): base (new BootstrapControlTemplateBinder(), new BootStrapWebRequestFactory(new List<IWebReference>()
        {
            new JavascriptReference("jquery-1.9.1.min.js"),
            new JavascriptReference("angular.min.js"),
            new JavascriptReference("bootstrap.min.js"),
            new JavascriptReference("OpenB.Controls.js"),
            new JavascriptReference("jquery-ui.min.js"),
            new CascadingStyleSheetReference("bootstrap-theme.min.css"),
            new CascadingStyleSheetReference("bootstrap.min.css"),  }))
        {
        }

       
    }
}