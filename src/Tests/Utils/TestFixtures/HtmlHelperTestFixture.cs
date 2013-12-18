using System.Web.Mvc;
using NUnit.Framework;
using Tests.Utils.WebFakers;

namespace Tests.Utils.TestFixtures
{
    public abstract class HtmlHelperTestFixture : BaseTestFixture
    {
        protected FakePrincipal CurrentUser = new FakePrincipal();
        protected HtmlHelper Html;

        [TestFixtureSetUp]
        public void TestSetup()
        {
            var viewContext = new ViewContext
            {
                HttpContext = new FakeHttpContext(CurrentUser)
            };

            Html = new HtmlHelper(viewContext, new FakeViewDataContainer());

        }

        [SetUp]
        public void Setup()
        {
            CurrentUser.ClearRoles();
        }

        
    }
}
