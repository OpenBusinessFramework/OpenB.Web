using OpenB.Web.Http;
using System;
using System.Xml;

namespace OpenB.Web.Content
{
    internal interface IObmlContentFactory
    {
        WebRequestOutput Create(string applicationPath, Uri uri, XmlDocument xmlDocument);
    }
}