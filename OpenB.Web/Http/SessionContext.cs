using OpenB.Web.Content.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace OpenB.Web.Http
{
    public class SessionContext
    {
        public string SessionId { get; private set; }
        private HttpContext httpContext;
        readonly SessionDataService sessionDataService;

        private SessionContext(HttpContext httpContext, SessionDataService sessionDataService)
        {            
            if (sessionDataService == null)
                throw new ArgumentNullException(nameof(sessionDataService));
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            this.httpContext = httpContext;
            this.sessionDataService = sessionDataService;

            if (httpContext.Request.Cookies["OpenB.SessionId"] != null)
            {
                SessionId = httpContext.Request.Cookies["OpenB.SessionId"].Value;
            }
        }

        public static SessionContext Create(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }            
         
            SessionIDManager sessionIdManager = new SessionIDManager();
            string sessionId = sessionIdManager.CreateSessionID(httpContext);
                

            return new SessionContext(httpContext, new SessionDataService(httpContext.Session));                                 
        }

        internal object GetBusinessObject(IDataBoundElement element)
        {
            return sessionDataService.GetBusinessObjectFor(element);
        }
    }

    public class SessionDataService 
    {
        IDictionary<string, IModel> sessionModelDictionary;     

        public SessionDataService(HttpSessionState sessionState)
        {          

            if (sessionState == null)
                throw new ArgumentNullException(nameof(sessionState));

            var sessionData = sessionState["sessionData"] as IDictionary<string, IModel>;

            if (sessionData == null)
            {
                sessionData = new Dictionary<string, IModel>();
                sessionState["sessionData"] = sessionData;
            }

            sessionModelDictionary = sessionData;
        }

        public object GetBusinessObjectFor(IDataBoundElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            throw new NotImplementedException();
        }
    }
}
