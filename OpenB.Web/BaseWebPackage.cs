using OpenB.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenB.Web
{
    public abstract class BaseWebPackage
    {
        public IWebControlTemplateBinder ControlTemplateBinder { get; private set; }
        public IWebRequestFactory WebRequestFactory { get; private set; }
       
        
        protected BaseWebPackage(IWebControlTemplateBinder controlTemplateBinder , IWebRequestFactory webRequestFactory)
        {
          
            if (webRequestFactory == null)
                throw new ArgumentNullException(nameof(webRequestFactory));
            if (controlTemplateBinder == null)
                throw new ArgumentNullException(nameof(controlTemplateBinder));

            ControlTemplateBinder = controlTemplateBinder;
            WebRequestFactory = webRequestFactory;
           
        }      
    }
}
