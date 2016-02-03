using System;
using System.Web.UI;

namespace OpenB.Web.Content
{
    public class RenderContext
    {
        public RenderContext(HtmlTextWriter textWriter, IWebReferenceService referenceService)
        {
            
            if (textWriter == null)
                throw new System.ArgumentNullException(nameof(textWriter));
            if (referenceService == null)
                throw new System.ArgumentNullException(nameof(referenceService));

            HtmlTextWriter = textWriter;
            ReferenceService = referenceService;
        }

       public HtmlTextWriter HtmlTextWriter { get; }
       public IWebReferenceService ReferenceService { get; }
       public Uri RequestUri { get; set; }
    }
}