using OpenB.Web;
using OpenB.Web.Content;
using OpenB.Web.Http;
using OpenB.WebPackages.BootStrap.FileHandlers;
using System.Collections.Generic;

namespace OpenB.WebPackages.BootStrap
{
    public class BootStrapWebRequestFactory : WebRequestFactory
    {
        public BootStrapWebRequestFactory(IEnumerable<IWebReference> webReferences) : base (new List<IWebRequestFileHandler>()
        {
            new CssFileHandler(),
            new JavaScriptFileHandler(),
            new Web.Http.FileHandlers.ObmlFileHandler(
                    new BootstrapControlTemplateBinder(), 
                    new WebReferenceService(webReferences)),
            new Web.Http.FileHandlers.IconFileHandler(),
            new MapFileHandler(),
        })
        {
        }
    }
}