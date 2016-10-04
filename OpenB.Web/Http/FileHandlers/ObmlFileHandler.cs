using OpenB.Web.Content;
using System;
using System.IO;
using System.Xml;

namespace OpenB.Web.Http.FileHandlers
{
    public class ObmlFileHandler : IWebRequestFileHandler
    {
        public string RequestPattern
        {
            get
            {
                return @".+\.obml";
            }
        }

        IObmlContentFactory contentFactory;
        
        public ObmlFileHandler(IWebControlTemplateBinder controlTemplateBinder, IWebReferenceService referenceService)
        {
            if (controlTemplateBinder == null)
                throw new ArgumentNullException(nameof(controlTemplateBinder));

            
            this.contentFactory = new ObmlContentFactory(controlTemplateBinder, referenceService);
        }

        public WebRequestOutput HandleRequest(WebRequestInput requestInput)
        {
            WebRequestOutput output = new WebRequestOutput();

            string contentFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, requestInput.RequestFileName.Remove(0,1));

            FileInfo obmlFileInfo = new FileInfo(contentFile);

            if (obmlFileInfo.Exists)
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(obmlFileInfo.FullName);

                WebRequestOutput content = contentFactory.Create(requestInput.ApplicationPath, requestInput.Url, xmlDocument);

                output.ContentType = content.ContentType;
                output.Response = content.Response;               
            }
            else
            {
                output.Error = new ResourceNotFoundError();
            }

            return output;
        }
    }
}