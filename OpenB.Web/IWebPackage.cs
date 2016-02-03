using OpenB.Web.Http;
using System.Collections.Generic;

namespace OpenB.Web
{
    public interface IWebPackage
    {
        IWebControlTemplateBinder ControlTemplateBinder { get; }
        IWebRequestFactory WebRequestFactory { get; }    
        
        IEnumerable<CascadingStyleSheetReference> CascadingStyleSheetReferences { get; }
        IEnumerable<JavascriptReference> JavascriptReferences { get; }

    }
}