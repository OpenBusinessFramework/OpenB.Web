using OpenB.Web;
using OpenB.Web.Http;
using System.IO;
using System.Reflection;

namespace OpenB.WebPackages.BootStrap.FileHandlers
{
    /// <summary>Supports all classes in the .NET Framework class hierarchy and provides low-level services to derived classes. This is the ultimate base class of all classes in the .NET Framework; it is the root of the type hierarchy.</summary>
    /// <filterpriority>1</filterpriority>
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
                        output.ContentType = "text/css";
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