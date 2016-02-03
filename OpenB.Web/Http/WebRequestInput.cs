using System;

namespace OpenB.Web.Http
{
    public class WebRequestInput
    {
        public string RequestExtension { get; internal set; }
        public string RequestFileName { get; internal set; }
        public Uri Url { get; internal set; }
    }
}