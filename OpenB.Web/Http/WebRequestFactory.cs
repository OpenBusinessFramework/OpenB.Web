using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenB.Web.Http
{
    public class WebRequestFactory : IWebRequestFactory
    {
        private IList<IWebRequestFileHandler> fileHandlers;

        public WebRequestFactory(IList<IWebRequestFileHandler> fileHandlers)
        {
            this.fileHandlers = fileHandlers;
            if (fileHandlers == null)
                throw new ArgumentNullException(nameof(fileHandlers));
        }

        public IWebRequestFileHandler GetFileHandlerForRequest(string request)
        {
            foreach(var fileHandler in fileHandlers)
            {
                var regularExpression = new Regex(fileHandler.RequestPattern);
                var match = regularExpression.Match(request);

                if (match.Success)
                {
                    return fileHandler;
                }
            }

            throw new NotSupportedException($"Request {request} is not supported.");
        }
    }
}