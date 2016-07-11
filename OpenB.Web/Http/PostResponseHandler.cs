using Newtonsoft.Json;
using System;
using System.IO;
using System.Web;

namespace OpenB.Web.Http
{
    internal class PostResponseHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private static PostResponseHandler instance;       

        internal static IHttpHandler GetInstance()
        {
            if (instance == null)
            {
                instance = new PostResponseHandler();
            }
            return instance;
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            //TODO: Allow other requesttypes.

            if (context.Request != null && context.Request.ContentType == "application/json; charset=utf-8")
            {
                SessionContext sessionContext = SessionContext.Create(context);

                StreamReader streamReader = new StreamReader(context.Request.InputStream);

                object businessObject = sessionContext.GetBusinessObject(null);

                string jsonString = streamReader.ReadToEnd();

               object thingy =  JsonConvert.DeserializeObject(jsonString);
            }
        }
    }
}