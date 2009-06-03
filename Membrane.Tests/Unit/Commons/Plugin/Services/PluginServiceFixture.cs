using System;
using System.Collections.Generic;
using System.Reflection;
using SystemWrapper;
using SystemWrapper.IO;
using SystemWrapper.Reflection;
using Castle.Windsor;
using Membrane.Commons;
using Membrane.Commons.Plugin.Services;
using Membrane.Commons.Plugin.Services.Interfaces;
using Membrane.Tests.Unit.Core;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Commons.Plugin.Services
{
	[TestFixture]
	public class PluginServiceFixture : BaseFixture
	{
		private IAssemblyWrap assembly;
		private IAssemblyNameWrap assemblyName;
		private IAppDomainWrap appDomain;
		private IFileWrap file;
		private IDirectoryWrap directory;

		private IPluginsService service;

		private const string PLUGINPATH = "/plugins";
		private const string PLUGINSEARCHPATTERN = "*.dll";

		public override void SetUp()
		{
			base.SetUp();

			assembly = mockery.Stub<IAssemblyWrap>();
			appDomain = mockery.Stub<IAppDomainWrap>();
			assemblyName = mockery.Stub<IAssemblyNameWrap>();
			file = mockery.Stub<IFileWrap>();
			directory = mockery.Stub<IDirectoryWrap>();

			service = new PluginsService(assembly, appDomain, assemblyName, file, directory);
		}

		[Test]
		public void CanCreateListOfAllCurrentlyFoundPlugins()
		{
			var pluginLibraries = new string[] {"plugins.dll" };

			IList<IMembranePlugin> result = null;
			var bytes = new byte[4];
			var name = "Membrane.Plugins";
			var foundAssemblyName = new AssemblyNameWrap("blog");
			var executingAssembly = mockery.Stub<IAssemblyWrap>();

			With.Mocks(mockery)
				.Expecting(() =>
				           	{
								Expect.Call(directory.GetFiles(PLUGINPATH, PLUGINSEARCHPATTERN)).IgnoreArguments().Return(pluginLibraries);
								Expect.Call(file.ReadAllBytes(null)).IgnoreArguments().Return(bytes);
				           		Expect.Call(appDomain.CurrentDomain).Return(new AppDomainWrap(AppDomain.CurrentDomain));
				           		Expect.Call(appDomain.GetAssemblies()).Return(new [] { executingAssembly });
				           		Expect.Call(assemblyName.GetAssemblyName("fileName")).Repeat.Any().IgnoreArguments().Return(foundAssemblyName);
				           		Expect.Call(assembly.Load(null)).IgnoreArguments().Return(new AssemblyWrap(Assembly.GetExecutingAssembly()));
								Expect.Call(assembly.GetExecutingAssembly()).Return(executingAssembly);
				           		Expect.Call(assembly.GetTypes()).Return(new [] {typeof (BlogPlugin), typeof (NewsPlugin)});
				           	})
				.Verify(() => result = service.FindAvailablePlugins());


		}
	}

	public class BlogPlugin : IMembranePlugin
	{
		public string Name
		{
			get { throw new NotImplementedException(); }
		}

		public void Initialize()
		{
		}

		public void RegisterComponents(IWindsorContainer container)
		{
		}

		public void Install()
		{
			throw new NotImplementedException();
		}

		public void Uninstall()
		{
			throw new NotImplementedException();
		}

		public void Upgrade()
		{
			throw new NotImplementedException();
		}
	}

	public class NewsPlugin : IMembranePlugin
	{
		public string Name
		{
			get { throw new NotImplementedException(); }
		}

		public void Initialize()
		{
		}

		public void RegisterComponents(IWindsorContainer container)
		{
		}

		public void Install()
		{
			throw new NotImplementedException();
		}

		public void Uninstall()
		{
			throw new NotImplementedException();
		}

		public void Upgrade()
		{
			throw new NotImplementedException();
		}
	}

}
