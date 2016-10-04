using System;
using System.Runtime.Serialization;

namespace OpenB.Web.Content
{  
    internal class ControlNotSupportedExecption : Exception
    {
        public string NodeName { get; private set; }

        public ControlNotSupportedExecption(string nodeName) : base($"Control {0} is not supported.")
        {
            NodeName = nodeName;        
        }        
    }
}