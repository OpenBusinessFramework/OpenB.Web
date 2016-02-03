using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenB.Web.Test
{
    [TestFixture]
    class RenderServiceTest
    {
        [Test]
        public void CreateNewRenderService_DoesNotThrowException()
        {
            MockRepository mockRepository = new MockRepository();
            IWebPackage webPackage = mockRepository.Stub<IWebPackage>();

            WebSolution webSolution = new WebSolution("MyFirstWebSolution", webPackage);

            Assert.DoesNotThrow(() => new RenderService(webSolution));          
        }
    }
}
