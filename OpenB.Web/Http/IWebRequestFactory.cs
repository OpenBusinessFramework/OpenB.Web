namespace OpenB.Web.Http
{
    public interface IWebRequestFactory
    {
        IWebRequestFileHandler GetFileHandlerForRequest(string requestExtension);
    }
}