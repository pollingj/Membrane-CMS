using Membrane.Controllers;
using Membrane.Core.Services.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Test.Unit.Web.MonoRail.Controllers
{
	[TestFixture]
	public class LoginControllerFixture : BaseControllerFixture
	{
		private IAuthenticationService service;
		private LoginController controller;

		public override void SetUp()
		{
			base.SetUp();

			service = mockery.DynamicMock<IAuthenticationService>();
			controller = new LoginController(service);

			PrepareController(controller);
		}

		[Test]
		public void ShouldAlertUserForInvalidLogin()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.AuthenticateUser("username", "password")).Return(null))
				.Verify(() => controller.Login("username", "password"));

			Assert.AreEqual(1, controller.Flash.Count, "Flash error is not being populate");
			Assert.AreEqual("Username or Password not recognised", controller.Flash["error"], "Flash is not being populated with correct message");
		}
	}
}