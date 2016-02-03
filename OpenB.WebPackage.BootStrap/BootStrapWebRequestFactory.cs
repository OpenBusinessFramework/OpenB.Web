using OpenB.Web.Http;
using OpenB.WebPackages.BootStrap.FileHandlers;
using System.Collections.Generic;

namespace OpenB.WebPackages.BootStrap
{
    public class BootStrapWebRequestFactory : WebRequestFactory
    {
        public BootStrapWebRequestFactory(): base (new List<IWebRequestFileHandler>()
        {new CssFileHandler(), new JavaScriptFileHandler(), new Web.Http.FileHandlers.ObmlFileHandler(new BootstrapControlTemplateBinder()), new Web.Http.FileHandlers.IconFileHandler()})
        {
        }
    }
}