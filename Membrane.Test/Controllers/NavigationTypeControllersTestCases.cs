using Castle.MonoRail.TestSupport;
using Membrane.Controllers;
using Membrane.Models.NavigationManagement;
using Membrane.Models.UserManagement;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Commons;

namespace Membrane.Test.Controllers
{
    [TestFixture]
    public class NavigationTypeControllersTestCases : BaseControllerTest
    {
        private MockRepository mockery;
        private NavigationType navType;
        private NavigationTypeController controller;
        private IRepository<User> user;

        [SetUp]
        public void SetUp()
        {
            mockery = new MockRepository();
            navType = mockery.DynamicMock<NavigationType>();
            controller = new NavigationTypeController();
            PrepareController(controller);
        }

        [Test]
        public void CanAddNavigationType()
        {
            controller.ShowNavigationTypes();
            user = mockery.DynamicMock<IRepository<User>>();
            



            Assert.IsNotNull(controller.PropertyBag["NavigationTypes"]);
        }
    }
}