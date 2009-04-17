using System.Collections.Generic;
using Castle.Windsor;
using Membrane.Commons;
using Membrane.Controllers.Administrator;
using Membrane.Core.Services.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.Administrator
{
	[TestFixture]
	public class PluginsControllerFixture : BaseControllerFixture
	{
		private IPluginsService service;
		private PluginsController controller;

		private const string PLUGINNAME = "Blog";

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			Referrer = "Plugins/List";

			service = mockery.DynamicMock<IPluginsService>();
			controller = new PluginsController(service);

			PrepareController(controller, "Plugins");
		}

		[Test]
		public void CanListAllPluginNamesFromPluginsFolder()
		{
			var plugins = new List<IMembranePlugin>
			              	{
			              		new TestBlogPlugin(),
								new TestNewsPlugin()
			              	};

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.FindAvailablePlugins()).Return(plugins))
				.Verify(() => controller.List());

			Assert.AreEqual(plugins, controller.PropertyBag["plugins"]);
			Assert.AreEqual(@"Plugins\Action", controller.SelectedViewName);
		}

		[Test]
		public void CanSuccessfullyInstallSelectedPlugin()
		{
			MockPluginInstall(true);

			Assert.IsNull(controller.Flash["error"]);
			Assert.AreEqual(Referrer, Response.RedirectedTo);
		}

		[Test]
		public void CanFailInstallingSelectedPlugin()
		{
			MockPluginInstall(false);

			Assert.IsNotNull(controller.Flash["error"]);
			Assert.AreEqual(Referrer, Response.RedirectedTo);
		}

		[Test]
		public void CanSuccessfullyUninstallSelectedPlugin()
		{
			MockPluginUninstall(true);

			Assert.IsNull(controller.Flash["error"]);
			Assert.AreEqual(Referrer, Response.RedirectedTo);
		}

		[Test]
		public void CanFailUninstallSelectedPlugin()
		{
			MockPluginUninstall(false);

			Assert.IsNotNull(controller.Flash["error"]);
			Assert.AreEqual(Referrer, Response.RedirectedTo);
		}

		[Test]
		public void CanSuccessfullyUpdgradePlugin()
		{
			MockPluginUpgrade(true);

			Assert.IsNull(controller.Flash["error"]);
			Assert.AreEqual(Referrer, Response.RedirectedTo);
		}

		[Test]
		public void CanFailUpdgradePlugin()
		{
			MockPluginUpgrade(false);

			Assert.IsNotNull(controller.Flash["error"]);
			Assert.AreEqual(Referrer, Response.RedirectedTo);
		}

		private void MockPluginInstall(bool success)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.InstallPlugin(PLUGINNAME)).Return(success))
				.Verify(() => controller.Install(PLUGINNAME));
		}

		private void MockPluginUninstall(bool success)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.UninstallPlugin(PLUGINNAME)).Return(success))
				.Verify(() => controller.Uninstall(PLUGINNAME));
		}

		private void MockPluginUpgrade(bool success)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.UpgradePlugin(PLUGINNAME)).Return(success))
				.Verify(() => controller.Upgrade(PLUGINNAME));
		}
	}

	public class TestBlogPlugin : IMembranePlugin
	{
		public string Name
		{
			get { return "Blog"; }
		}

		public void Initialize()
		{
		}

		public void Install()
		{
		}

		public void Uninstall()
		{
		}

		public void Upgrade()
		{
		}

		public void RegisterComponents(IWindsorContainer container)
		{
		}
	}

	public class TestNewsPlugin : IMembranePlugin
	{
		public string Name
		{
			get { return "News"; }
		}

		public void Initialize()
		{
		}

		public void Install()
		{
		}

		public void Uninstall()
		{
		}

		public void Upgrade()
		{
		}

		public void RegisterComponents(IWindsorContainer container)
		{
		}
	}
}