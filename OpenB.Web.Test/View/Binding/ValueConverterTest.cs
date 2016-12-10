using NUnit.Framework;
using OpenB.Web.View.Binding;

namespace OpenB.Web.Test.View.Binding
{
    [TestFixture]
    public class ValueConverterTest
    {
        [Test]
        public void DoSomething()
        {
            var valueConverterFactory = ValueConverterFactory.GetInstance();

            bool source = false;


            //string booleanString = valueConverterFactory.GetDisplayValue<string>(source);
        }
    }
}
