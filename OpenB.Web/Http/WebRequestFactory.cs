using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OpenB.Web.Http
{
    public class WebRequestFactory : IWebRequestFactory
    {
        private IList<IWebRequestFileHandler> fileHandlers;

        public WebRequestFactoryConfiguration Configuration { get; private set; }

        public WebRequestFactory(IList<IWebRequestFileHandler> fileHandlers) : this(WebRequestFactoryConfiguration.GetInstance(), fileHandlers)
        {

        }

        public WebRequestFactory(WebRequestFactoryConfiguration configuration, IList<IWebRequestFileHandler> fileHandlers)
        {
            Configuration = configuration;
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            this.fileHandlers = fileHandlers;
            if (fileHandlers == null)
                throw new ArgumentNullException(nameof(fileHandlers));
        }

        public IWebRequestFileHandler GetFileHandlerForRequest(string request)
        {          

            foreach (var fileHandler in fileHandlers)
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

    public class WebRequestFactoryConfiguration
    {
        public static WebRequestFactoryConfiguration GetInstance()
        {
            return new WebRequestFactoryConfiguration { DefaultLandingPage = "/MainPage.obml" };
        }
        public string DefaultLandingPage { get; set; }
    }
}