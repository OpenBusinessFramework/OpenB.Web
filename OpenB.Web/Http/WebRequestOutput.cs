namespace OpenB.Web.Http
{
    public class WebRequestOutput
    {
        public string Response { get; set; }
        public WebRequestError Error { get; set; }
        public bool HasError { get { return Error != null; } }
        public string ContentType { get;  set; }
    }
}