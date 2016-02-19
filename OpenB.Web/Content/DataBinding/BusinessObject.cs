using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenB.Web.Content.DataBinding
{
    public class BusinessObject
    {
        public IEnumerable<Parameter> Parameters { get; set; }
        public ServiceReference ServiceReference { get; set; }
    }

    public class ServiceReference
    {
        public string name { get; }
        public MethodReference InitializationMethod { get; set; }      
    }

    public class MethodReference
    {
        public IEnumerable<Parameter> Parameters { get; set; }
        public string name { get; }
    }

    public class Parameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
