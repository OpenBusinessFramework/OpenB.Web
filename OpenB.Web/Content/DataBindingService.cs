using OpenB.Web.View.Binding;
using System.Collections.Generic;
using System;
using OpenB.Web.Content.Elements;

namespace OpenB.Web.Content
{
  

    public class Binding
    {
        public Binding(IElement element, object model, string propertyPath)
        {
            if (propertyPath == null)
                throw new ArgumentNullException(nameof(propertyPath));
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (element == null)
                throw new ArgumentNullException(nameof(element));
        }
    }

    public class DataBindingService
    {
        IDictionary<string, ISimpleValueConverter> converters;
        ValueConverterFactory converterFactory;

        public DataBindingService()
        {
            converters = new Dictionary<string, ISimpleValueConverter>();
            converterFactory = ValueConverterFactory.GetInstance();
        }

        

        public object GetDisplayValue(object element, string propertyPath)
        {
            //    converters[propertyPath].GetType().InvokeMember("ConvertBack", )
            throw new NotImplementedException();
        }


    }
}