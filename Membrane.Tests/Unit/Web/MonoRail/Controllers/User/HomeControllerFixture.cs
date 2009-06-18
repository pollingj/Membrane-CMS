using System;
using System.Collections.Generic;
using Membrane.Controllers.User;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.User
{
	[TestFixture]
	public class HomeControllerFixture : BaseControllerFixture
	{
		private HomeController controller;
		private IPluginsService service;

		public override void TestFixtureSetUp()
		{
			base.TestFixtureSetUp();

			service = mockery.DynamicMock<IPluginsService>();

			controller = new HomeController(service);

			PrepareController(controller);
		}

		[Test]
		public void HomePageCanShowContentTypes()
		{
			var installedPlugins = new List<InstalledPluginDTO>
			                       	{
			                       		new InstalledPluginDTO {Id = Guid.NewGuid(), Name = "News", Version = "1.0.0"},
			                       		new InstalledPluginDTO {Id = Guid.NewGuid(), Name = "Blog", Version = "1.0.1"},
			                       		new InstalledPluginDTO {Id = Guid.NewGuid(), Name = "Exhibitions", Version = "1.0.0"}
			                       	};
				With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.GetAllInstalledPlugins()).Return(installedPlugins))
				.Verify(() => controller.Index());

			Assert.AreEqual(installedPlugins, controller.PropertyBag["InstalledPlugins"]);
		}

		[Test]
		public void CanRedirectToPluginNewAction()
		{
			var controllerName = "Blog";
			controller.GoToPluginCreation(controllerName);

			Assert.AreEqual("/Blog/New.castle", Response.RedirectedTo);
		}
	}
}