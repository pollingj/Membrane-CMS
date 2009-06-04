using System;
using System.Collections.Generic;
using System.Configuration;
using Membrane.Commons;
using Membrane.Controllers.Administrator;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using Membrane.Tests.Unit.TestPlugins;
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
		private const string PLUGINPATH = "/plugins";

		private Guid PluginId = Guid.NewGuid();
		public override void TestFixtureSetUp()
		{
			base.TestFixtureSetUp();

			Referrer = "Plugins/List";

			service = mockery.DynamicMock<IPluginsService>();
			controller = new PluginsController(service);

			ConfigurationManager.AppSettings["plugins.path"] = PLUGINPATH;

			PrepareController(controller, "Plugins");



		}

		[Test]
		public void CanListAllPluginNamesFromPluginsFolderAndAllInstalledPlugins()
		{
			var plugins = new List<IMembranePlugin>
			              	{
			              		new TestBlogPlugin(),
								new TestNewsPlugin()
			              	};

			var installedPlugins = new List<InstalledPluginDTO>
			                       	{
			                       		new InstalledPluginDTO {Id = Guid.NewGuid(), Name = "Blog", Version = "1.0.0"},
			                       		new InstalledPluginDTO {Id = Guid.NewGuid(), Name = "News", Version = "2.0.0"}
			                       	};

			With.Mocks(mockery)
				.Expecting(() =>
				           	{
				           		Expect.Call(service.FindAvailablePlugins(PLUGINPATH)).Return(plugins);
				           		Expect.Call(service.GetAllInstalledPlugins()).Return(installedPlugins);
				           	})
				.Verify(() => controller.List());

			Assert.AreEqual(plugins, controller.PropertyBag["plugins"]);
			Assert.AreEqual(installedPlugins, controller.PropertyBag["installedplugins"]);
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
				.Expecting(() => Expect.Call(service.InstallPlugin(PLUGINNAME, PLUGINPATH)).Return(success))
				.Verify(() => controller.Install(PLUGINNAME));
		}

		private void MockPluginUninstall(bool success)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.UninstallPlugin(PluginId, PLUGINPATH)).Return(success))
				.Verify(() => controller.Uninstall(PluginId));
		}

		private void MockPluginUpgrade(bool success)
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(service.UpgradePlugin(PluginId, PLUGINPATH)).Return(success))
				.Verify(() => controller.Upgrade(PluginId));
		}
	}


}