namespace OpenB.Web.Http
{
    public interface IWebRequestFileHandler
    {
        WebRequestOutput HandleRequest(WebRequestInput requestInput);        
        string RequestPattern { get; }
    }
}