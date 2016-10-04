using System;
using System.Reflection;

namespace OpenB.Web.Content.DataBinding
{
    public class Binder
    {
        readonly DataBindingConfiguration configuration;

        public Binder(DataBindingConfiguration configuration)
        {
            this.configuration = configuration;
            if (configuration == null)
                throw new System.ArgumentNullException(nameof(configuration));
        }

        public object GetValue(string bindingPath, object model)
        {
            string[] levels = bindingPath.Split('.');

            object currentValue = model;

            foreach (string level in levels)
            {
                PropertyInfo property = currentValue.GetType().GetProperty(level);

                if (property == null)
                {
                    throw new Exception($"Property {level} or field not found on type {currentValue.GetType()}.");
                }

                currentValue = property.GetValue(currentValue);
            }

            return currentValue;
        }
    }

    public class DataBindingConfiguration
    {
        public string ModelNameSpace { get; set; }
    }
}
