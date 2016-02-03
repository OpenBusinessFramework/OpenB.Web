using System.Collections.Generic;

namespace OpenB.Web.Content
{
    public interface IWebReferenceService
    {
        IEnumerable<string> GetLinks(string host);
    }
}