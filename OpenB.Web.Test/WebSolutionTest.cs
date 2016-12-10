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
    public class WebSolutionTest
    {
        [Test]
        public void CreateNewWebSolution_NoName_Throws_Exception()
        {
            MockRepository mockRepository = new MockRepository();
            IWebPackage webPackage = mockRepository.Stub<IWebPackage>();

            var configuration = mockRepository.Stub<WebSolutionConfiguration>();

            Assert.Throws<NotSupportedException>(() => new WebSolution(configuration));
        }

        [Test]
        public void CreateNewWebSolution_NoWebPackage_Throws_Exception()
        {
            MockRepository mockRepository = new MockRepository();
            IWebPackage webPackage = mockRepository.Stub<IWebPackage>();

            var configuration = mockRepository.Stub<WebSolutionConfiguration>();

            Assert.Throws<ArgumentNullException>(() => new WebSolution(configuration));
        }
    }
}
