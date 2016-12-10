using System;

namespace OpenB.Web
{
    internal class UrlMapper : IUrlMapper
    {
        public string DefaultUrl { get; private set; }

        public UrlMapper(string defaultUrl)
        {
            DefaultUrl = defaultUrl;
            if (defaultUrl == null)
                throw new ArgumentNullException(nameof(defaultUrl));
        }

        public string Map(string originalUrl)
        {
            var newUrl = originalUrl.Remove(0, 1);

            if (newUrl.Equals("") || newUrl.Equals("/"))
            {
                newUrl = DefaultUrl;
            }

            return newUrl;            
        }
    }
}