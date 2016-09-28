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
            this.sessionDataService = sessionDataService;
            if (sessionDataService == null)
                throw new ArgumentNullException(nameof(sessionDataService));
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            this.httpContext = httpContext;

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

            sessionModelDictionary = sessionState["sessionModels"] as IDictionary<string, IModel>;
        }

        public object GetBusinessObjectFor(IDataBoundElement element)
        {
            throw new NotImplementedException();
        }
    }
}
