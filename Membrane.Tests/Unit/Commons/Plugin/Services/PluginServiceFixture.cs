using System.Collections.Generic;
using Membrane.Commons;
using Membrane.Commons.Plugin.Services;
using Membrane.Commons.Wrappers.Interfaces;
using Membrane.Tests.Unit.Core;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Commons.Plugin.Services
{
	[TestFixture]
	public class PluginServiceFixture : BaseFixture
	{
		private IDirectory directory;
		private IAssembly assembly;

		private IPluginsService service;

		private const string PLUGINPATH = "/plugins";
		private const string PLUGINSEARCHPATTERN = "*.dll";

		public override void SetUp()
		{
			base.SetUp();

			directory = mockery.DynamicMock<IDirectory>();
			assembly = mockery.DynamicMock<IAssembly>();

			service = new PluginsService(assembly, directory);
		}

		[Test]
		public void CanCreateListOfAllCurrentlyFoundPlugins()
		{
			var pluginLibraries = new string[] {"blog.dll", "news.dll"};
			IList<IMembranePlugin> result = null;
			var executingAssembly = mockery.Stub<IAssembly>();
			executingAssembly.FullName = "Membrane.Commons";

			With.Mocks(mockery)
				.Expecting(() =>
				           	{
				           		Expect.Call(directory.GetFiles(PLUGINPATH, PLUGINSEARCHPATTERN)).Return(pluginLibraries);
				           		Expect.Call(assembly.GetExecutingAssembly()).Return(executingAssembly);
				           	})
				.Verify(() => result = service.FindAvailablePlugins());


		}
	}
}