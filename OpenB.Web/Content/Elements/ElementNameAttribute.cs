using System;

namespace OpenB.Web.Content.Elements
{
    internal class ElementNameAttribute : Attribute
    {
        public string ElementName
        {
            get;
            private set;
        }

        public ElementNameAttribute(string elementName)
        {
            this.ElementName = elementName;
        }
    }
}