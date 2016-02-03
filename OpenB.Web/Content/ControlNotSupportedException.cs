using System;
using System.Runtime.Serialization;

namespace OpenB.Web.Content
{
    [Serializable]
    internal class ControlNotSupportedException : Exception
    {
        public ControlNotSupportedException()
        {
        }

        public ControlNotSupportedException(string message) : base(message)
        {
        }

        public ControlNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ControlNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}