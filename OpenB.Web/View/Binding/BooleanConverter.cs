using System;

namespace OpenB.Web.View.Binding
{
    public class DateToStringConverter : ISimpleValueConverter<string, DateTime>
    {
        public string Convert(DateTime modelValue)
        {
            return modelValue.ToShortDateString();
        }

        public DateTime ConvertBack(string displayValue)
        {
            return System.Convert.ToDateTime(displayValue);
        }
    }

    public class StringToDateConverter : ISimpleValueConverter<DateTime, string>
    {
        public DateTime Convert(string modelValue)
        {
            return System.Convert.ToDateTime(modelValue);
        }

        public string ConvertBack(DateTime displayValue)
        {
            return displayValue.ToShortDateString();
        }
    }

    public class StringToBooleanConverter : ISimpleValueConverter<string, bool> 
    {
        public string Convert(bool modelValue)
        {
            return modelValue ? "Yes" : "No";
        }

        public bool ConvertBack(string displayValue)
        {
            return System.Convert.ToBoolean(displayValue);
        }
    }
}
