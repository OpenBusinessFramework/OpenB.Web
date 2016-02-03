using OpenB.Web.Http;

namespace OpenB.Web.Content
{
    internal class ControlNotSupportedError : WebRequestError
    {
        private string nodeName;

        public ControlNotSupportedError(string nodeName)
        {
            this.nodeName = nodeName;
        }
    }
}