using System;

namespace OpenB.Web.Content.Elements
{
    internal class AttributeNameAttribute : Attribute
    {
        public string Name { get; }
        public bool IsParseble { get;  }

        public AttributeNameAttribute(string name) : this(name, false)
        {
       
        }      

        public AttributeNameAttribute(string name, bool isParseble)
        {
            IsParseble = isParseble;
            Name = name;
        }
    }
}