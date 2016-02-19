using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenB.Web.Content
{
    public class WebReferenceService : IWebReferenceService
    {
        readonly IEnumerable<IWebReference> webReferences;
      
        public WebReferenceService(IEnumerable<IWebReference> webReferences)
        {
            if (webReferences == null)
                throw new ArgumentNullException(nameof(webReferences));

            this.webReferences = webReferences;    
        }


        public IEnumerable<string> GetLinks(string protocol, string hostApplicationPath)
        {
            foreach(IWebReference webReference in webReferences)
            {
                yield return webReference.GetWebReference($"{protocol}://{hostApplicationPath}");
            }
        }
    }
}
