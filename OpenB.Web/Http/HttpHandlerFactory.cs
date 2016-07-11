using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OpenB.Web.Http
{
    class HttpHandlerFactory : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            Configuration configuration = ConfigurationFactory.GetConfiguration();

            var contextRequestType = context.Request.RequestType.ToLower();

            switch (contextRequestType)
            {
                case "get":                
                    return GetResponseHandler.GetInstance();
                case "post":
                    return PostResponseHandler.GetInstance();
                default:
                    throw new NotSupportedException($"RequestType {contextRequestType} is not supported.");
            }           
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
           
        }        
    }
}
