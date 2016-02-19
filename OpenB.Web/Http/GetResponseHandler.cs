using System;
using System.Web;

namespace OpenB.Web.Http
{
    internal class GetResponseHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private GetResponseHandler()
        {
           WebSolution = WebSolutionFactory.CreateSolution();          
        }

        public WebSolution WebSolution {get; private set; }

        private static GetResponseHandler instance;

        public static GetResponseHandler GetInstance()
        {
            if (instance == null)
            {
                instance = new GetResponseHandler();
            }
            return instance;
        }

        public string RequestFileName { get; private set; }

        public void ProcessRequest(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var extension = context.Request.CurrentExecutionFilePathExtension;
            var filename = context.Request.AppRelativeCurrentExecutionFilePath.Remove(0,1);

            WebRequestInput webRequestInput = new WebRequestInput()
            {
                ApplicationPath = context.Request.ApplicationPath,
                RequestExtension = extension.Replace(".", string.Empty),
                RequestFileName = filename,
                Url = context.Request.Url
            };

            IWebRequestFactory webRequestFactory = WebSolution.WebPackage.WebRequestFactory;

            try
            {
                var fileHandler = webRequestFactory.GetFileHandlerForRequest(filename);


                var result = fileHandler.HandleRequest(webRequestInput);

                if (!result.HasError)
                {
                    context.Response.ContentType = result.ContentType;
                    context.Response.Output.Write(result.Response);
                } 
                else
                {
                    if (result.Error is ResourceNotFoundError)
                    {
                        context.Response.StatusCode = 404;
                    }                   
                }
            }
            
            catch(NotSupportedException nsEx)
            {
                context.Response.StatusCode = 501;
            }      

         }
    }
}