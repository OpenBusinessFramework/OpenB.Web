using NUnit.Framework;
using OpenB.Core;
using OpenB.Web.View;

namespace OpenB.Web.Test.View
{
    public class PageGenerationService
    {

    }



    [TestFixture]
    public class ViewModelServiceTest
    {
        [Test]
        public void DoSomething()
        {

            TestModel user = new TestModel();

            ViewModelGenerator service = new ViewModelGenerator(@"E:\Downloads\Projects\Projects\Personal\OpenB.Web\OpenB.Web.Test\View\Configuration\");
            MappingConfiguration mappingConfiguration = new MappingConfiguration(@"E:\Downloads\Projects\Projects\Personal\OpenB.Web\OpenB.Web.Test\View\Configuration\ViewModel.Mapping.xml");

            IViewModel resultModel = service.Generate(typeof(TestModel), mappingConfiguration);


        }

    }

    public class TestModel : IModel
    {
        public string Description
        {
            get; set;
        }

        public bool IsActive
        {
            get; set;
        }

        public string Key
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
    }
}
