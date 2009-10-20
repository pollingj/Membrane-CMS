using System;
using System.Collections.Generic;
using Membrane.Commons.Plugin.DTOs.Interfaces;
using Membrane.Controllers.User;
using Membrane.Core.DTOs;
using Membrane.Core.Services.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Web.MonoRail.Controllers.User
{
	[TestFixture]
	public class ContentControllerFixture : BaseControllerFixture
	{
		private IPluginsService pluginService;
		private ContentController controller;

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			pluginService = mockery.DynamicMock<IPluginsService>();

			controller = new ContentController(pluginService);

			PrepareController(controller);
		}

		[Test]
		public void CanShowFirstPluginContent()
		{
			var installedPlugins = new List<InstalledPluginDTO>
			                       	{
			                       		new InstalledPluginDTO {Id = Guid.NewGuid(), Name = "News", Version = "1.0.0"},
			                       		new InstalledPluginDTO {Id = Guid.NewGuid(), Name = "Blog", Version = "1.0.1"},
			                       		new InstalledPluginDTO {Id = Guid.NewGuid(), Name = "Exhibitions", Version = "1.0.0"}
			                       	};

			With.Mocks(mockery)
				.Expecting(() => Expect.Call(pluginService.GetAllInstalledPlugins()).Return(installedPlugins))
				.Verify(() => controller.GoToFirstPluginList());

			Assert.AreEqual("/News/List.castle", Response.RedirectedTo);
		}

		[Test]
		public void CanShowNoPluginsInstalledMessage()
		{
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(pluginService.GetAllInstalledPlugins()).Return(new List<InstalledPluginDTO>() {}))
				.Verify(() => controller.GoToFirstPluginList());

			Assert.IsTrue((bool)controller.PropertyBag["NoPlugins"]);
		}
	}

	public class NewsArticleDTO : IDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Story { get; set; }
		public DateTime CreationDate { get; set; }
	}
}