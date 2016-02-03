using OpenB.Web;

namespace OpenB.WebPackages.BootStrap
{
    public class BootStrapWebPackage : BaseWebPackage, IWebPackage
    {
        public BootStrapWebPackage(): base (new BootstrapControlTemplateBinder(), new BootStrapWebRequestFactory())
        {
        }
    }
}