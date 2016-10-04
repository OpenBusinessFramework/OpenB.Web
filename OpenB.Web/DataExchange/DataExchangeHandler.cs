using OpenB.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenB.Web.DataExchange
{
    /// <summary>
    /// This class handles the response requests from obml generated webpages.
    /// 
    /// </summary>
    public class DataExchangeHandler : IWebRequestFileHandler
    {
        public string RequestPattern
        {
            get
            {
                return @".+\.obdh";
            }
        }

        public WebRequestOutput HandleRequest(WebRequestInput requestInput)
        {
            if (requestInput == null)
                throw new ArgumentNullException(nameof(requestInput));

            return new WebRequestOutput();
        }
    }
}
