using Membrane.Commons.Wrappers.Interfaces;
using Membrane.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers
{
	[TestFixture]
	public class LogOffControllerFixture : BaseControllerFixture
	{
		private LogOffController controller;
		private IFormsAuthentication formsAuthentication;

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();


			formsAuthentication = mockery.Stub<IFormsAuthentication>();

			controller = new LogOffController(formsAuthentication);
			

			PrepareController(controller);
		}

		[Test]
		public void UserCanSuccessfullyLogOff()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(() => formsAuthentication.SignOut()))
				.Verify(() => controller.LogOff());

			Assert.AreEqual("/", Response.RedirectedTo);
		}
	}
}