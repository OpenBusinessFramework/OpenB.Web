using System;
using System.Web.UI;

namespace OpenB.Web.Content
{
    public class RenderContext
    {
        

        public RenderContext(HtmlTextWriter textWriter, IWebReferenceService referenceService, Uri requestUri, string applicationPath)
        {           
            if (requestUri == null)
                throw new ArgumentNullException(nameof(requestUri));
            if (textWriter == null)
                throw new System.ArgumentNullException(nameof(textWriter));
            if (referenceService == null)
                throw new System.ArgumentNullException(nameof(referenceService));

            HtmlTextWriter = textWriter;
            ReferenceService = referenceService;
            RequestUri = requestUri;
            ApplicationPath = applicationPath;
        }

        public string ApplicationPath { get; }
       public HtmlTextWriter HtmlTextWriter { get; }
       public IWebReferenceService ReferenceService { get; }
       public Uri RequestUri { get; private set; }
    }
}