using System;

namespace OpenB.Web.Content.Elements
{
    internal class AttributeNameAttribute : Attribute
    {
        public string Name { get; }

        public AttributeNameAttribute(string name)
        {
            Name = name;
        }
    }
}