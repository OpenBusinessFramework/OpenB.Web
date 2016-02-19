using System;
using System.Runtime.Serialization;

namespace OpenB.Web.Content
{
    [Serializable]
    internal class AttributeNotSupportedException : Exception
    {
        public AttributeNotSupportedException()
        {
        }

        public AttributeNotSupportedException(string message) : base(message)
        {
        }

        public AttributeNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AttributeNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}