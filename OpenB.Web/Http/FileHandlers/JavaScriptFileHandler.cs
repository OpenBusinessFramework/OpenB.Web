using System;

namespace OpenB.Web.Http.FileHandlers
{
    public class JavaScriptFileHandler : IWebRequestFileHandler
    {
        public string RequestPattern
        {
            get
            {
                return @".+\.js";
            }
        }      

        public WebRequestOutput HandleRequest(WebRequestInput requestInput)
        {
            throw new NotImplementedException();
        }
    }
}