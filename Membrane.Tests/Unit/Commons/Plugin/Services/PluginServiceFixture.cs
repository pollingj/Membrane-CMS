using System.Collections.Generic;
using System.Reflection;
using Membrane.Commons;
using Membrane.Commons.Plugin;
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
		private IAssemblyLoader assemblyLoader;
		private IFileSystem fileSystem;

		private IPluginsService service;

		private const string PLUGINPATH = "/plugins";
		private const string PLUGINSEARCHPATTERN = "*.dll";

		public override void SetUp()
		{
			base.SetUp();

			assemblyLoader = mockery.Stub<IAssemblyLoader>();
			fileSystem = mockery.Stub<IFileSystem>();

			service = new PluginsService(assemblyLoader, fileSystem);
		}

		[Test]
		public void CanCreateListOfAllCurrentlyFoundPlugins()
		{
			var pluginLibraries = new string[] {"blog.dll", "news.dll"};
			
			IList<IMembranePlugin> result = null;
			var bytes = new byte[4];
			var name = "Membrane.Plugins";
			var assemblyName = new AssemblyName("blog");
			var executingAssembly = mockery.Stub<IAssemblyLoader>();

			With.Mocks(mockery)
				.Expecting(() =>
				           	{
								Expect.Call(fileSystem.GetFiles(PLUGINPATH, PLUGINSEARCHPATTERN)).IgnoreArguments().Return(pluginLibraries);
								Expect.Call(fileSystem.ReadAllBytes(null)).IgnoreArguments().Return(bytes);
				           		Expect.Call(assemblyLoader.GetAssemblyName("fileName")).Repeat.Any().IgnoreArguments().Return(assemblyName);
								Expect.Call(assemblyLoader.GetExecutingAssembly()).Return(executingAssembly);
				           	})
				.Verify(() => result = service.FindAvailablePlugins());


		}
	}
}