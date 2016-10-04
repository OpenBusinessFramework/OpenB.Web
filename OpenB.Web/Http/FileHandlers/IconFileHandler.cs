using System;
using System.IO;
using System.Reflection;

namespace OpenB.Web.Http.FileHandlers
{
    public class IconFileHandler : IWebRequestFileHandler
    {
        public string RequestPattern
        {
            get
            {
                return @".+\.ico";
            }
        }

        WebRequestOutput IWebRequestFileHandler.HandleRequest(WebRequestInput requestInput)
        {
            WebRequestOutput output = new WebRequestOutput();
            Assembly assembly = Assembly.GetAssembly(this.GetType());
            var resourceName = $"{assembly.GetName().Name}.{requestInput.RequestFileName.Replace('/', '.').Replace("..", ".")}";
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                using (stream)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        output.ContentType = "image/x-icon";
                        output.Response = result;
                    }
                }
            }
            else
            {
                output.Error = new ResourceNotFoundError();
            }

            return output;
        }
    }
}