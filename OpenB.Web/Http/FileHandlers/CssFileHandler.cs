using System;
using System.IO;
using System.Reflection;

namespace OpenB.Web.Http.FileHandlers
{
    public class CssFileHandler : IWebRequestFileHandler
    {
        public string RequestPattern
        {
            get
            {
                return @".+\.css";
            }
        }

        public WebRequestOutput HandleRequest(WebRequestInput requestInput)
        {
            throw new NotImplementedException();
         
        }        
    }   
}