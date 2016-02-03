using OpenB.Web;
using OpenB.Web.Http;
using System.IO;
using System.Reflection;

namespace OpenB.WebPackages.BootStrap.FileHandlers
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
            WebRequestOutput output = new WebRequestOutput();
            Assembly assembly = Assembly.GetAssembly(this.GetType());
            var resourceName = $"{assembly.GetName().Name}.Content{requestInput.RequestFileName.Replace('/', '.').Replace("..", ".")}";
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                using (stream)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string result = reader.ReadToEnd();
                        output.ContentType = "text/javascript";
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