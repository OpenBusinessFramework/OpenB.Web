using OpenB.Web.Util.Reflection;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace OpenB.Web.View.Binding
{
    public class ValueConverterFactory
    {
        readonly CultureInfo cultureInfo;

        private ValueConverterFactory(CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
                throw new ArgumentNullException(nameof(cultureInfo));

            this.cultureInfo = cultureInfo;
        }

        /// <summary>
        /// Converts the give value to the given destination type. 
        ///         
        /// When trying to convert the value, firstly this function will look for a <see cref="ISimpleValueConverter{TSource, TDestination}" /> which can convert to the give <see cref="Type"/>,
        /// if there is no converter available then the value will be converted to a <see cref="string"/>.
        /// If the source and destination type are the same, the value itself will be returned.
        /// </summary>
        /// <param name="destinationType">Type which the given value should be converted to.</param>
        /// <param name="value">The provided value to convert.</param>
        /// <returns></returns>
        public object ConvertTo(Type destinationType, object value)
        {
            if (value == null)
                return null;

            if (destinationType == value.GetType())
                return value;

            Type inferfaceType = typeof(ISimpleValueConverter<,>).MakeGenericType(new[] { destinationType, value.GetType() });

            TypeSearchOptions typeSearchOptions = new TypeSearchOptions()
            {
                ImplementsInterfaces = new[] { inferfaceType }
            };

            Type converterType = Reflector.GetTypes(this.GetType().Assembly, typeSearchOptions).SingleOrDefault();

            if (converterType == null)
            {
                return (string)value;
            }

            object converter = Activator.CreateInstance(converterType);

            MethodInfo method = converterType.GetMethod("Convert");
            return method.Invoke(converter, new[] { value });
        }


        public static ValueConverterFactory GetInstance()
        {
            return new ValueConverterFactory(CultureInfo.CurrentCulture);
        }
    }

    public interface ISimpleValueConverter
    {
        
    }
}
