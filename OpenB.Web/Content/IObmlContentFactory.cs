using OpenB.Web.Http;
using System.Xml;

namespace OpenB.Web.Content
{
    internal interface IObmlContentFactory
    {
        WebRequestOutput Create(XmlDocument xmlDocument);
    }
}