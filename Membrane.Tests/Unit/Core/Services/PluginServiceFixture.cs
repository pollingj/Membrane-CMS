using System;
using System.Collections.Generic;
using System.Reflection;
using SystemWrapper;
using SystemWrapper.IO;
using SystemWrapper.Reflection;
using Castle.Windsor;
using Membrane.Commons;
using Membrane.Commons.Persistence;
using Membrane.Core.DTOs;
using Membrane.Core.Queries.Plugin;
using Membrane.Core.Services;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;
using Membrane.Tests.Unit.TestPlugins;
using NUnit.Framework;
using Rhino.Mocks;

namespace Membrane.Tests.Unit.Core.Services
{
	[TestFixture]
	public class PluginServiceFixture : BaseFixture
	{
		private IAssemblyWrap assembly;
		private IAssemblyNameWrap assemblyName;
		private IAppDomainWrap appDomain;
		private IFileWrap file;
		private IDirectoryWrap directory;
		private IWindsorContainer container;
		private IRepository<InstalledPlugin> repository;

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
			container = mockery.DynamicMock<IWindsorContainer>();
			repository = mockery.DynamicMock<IRepository<InstalledPlugin>>();

			service = new PluginsService(assembly, appDomain, assemblyName, file, directory, container, repository);
		}

		[Test]
		public void CanCreateListOfAllCurrentlyFoundPlugins()
		{
			var expectedPluginCount = 2;
			IList<IMembranePlugin> result = null; 
			var mocked = mockFindAvailablePlugins(false);

			mocked.Verify(() => result = service.FindAvailablePlugins(PLUGINPATH));

			Assert.AreEqual(expectedPluginCount, result.Count);
		}

		[Test]
		public void CanGetCurrentlyInstalledPlugins()
		{
			var blogGuid = Guid.NewGuid();
			var newsGuid = Guid.NewGuid();
			IList<InstalledPluginDTO> results = null;
			var installedPlugins = new List<InstalledPlugin>
			                       	{
			                       		new InstalledPlugin {Id = blogGuid, Name = "Blog", Version = "1.0.0"},
			                       		new InstalledPlugin {Id = newsGuid, Name = "News", Version = "2.0.0"}
			                       	};

			var installedPluginsDTO = new List<InstalledPluginDTO>
			                          	{
			                          		new InstalledPluginDTO {Id = blogGuid, Name = "Blog", Version = "1.0.0"},
			                          		new InstalledPluginDTO {Id = newsGuid, Name = "News", Version = "2.0.0"}
			                          	};
			With.Mocks(mockery)
				.Expecting(() => Expect.Call(repository.Find(new OrderedPlugins())).IgnoreArguments().Return(installedPlugins))
				.Verify(() => results = service.GetAllInstalledPlugins());

			Assert.AreEqual(installedPluginsDTO.Count, results.Count);

		}

		[Test]
		public void CanSuccessfullyInstallPlugin()
		{
			var mocked = mockFindAvailablePlugins(true);

			var result = verifyInstallPlugin("Test Blog", mocked);

			Assert.IsTrue(result);
		}


		[Test]
		public void CanFailInstallingPlugin()
		{
			var result = verifyInstallPlugin("None Existant Plugin", mockFindAvailablePlugins(false));

			Assert.IsFalse(result);
		}

		[Test]
		public void CanSuccessfullyUninstallPlugin()
		{
			var pluginId = Guid.NewGuid();
			var result = false;

			var pluginLibraries = new[] { "plugins.dll" };

			var bytes = new byte[4];
			var foundAssemblyName = new AssemblyNameWrap("blog");
			var executingAssembly = mockery.Stub<IAssemblyWrap>();

			With.Mocks(mockery)
				.Expecting(() =>
				{
					Expect.Call(repository.FindById(pluginId)).Return(new InstalledPlugin { Id = pluginId, Name = "Test Blog", Version = "2.0.0" });
					Expect.Call(directory.GetFiles(PLUGINPATH, PLUGINSEARCHPATTERN)).IgnoreArguments().Return(pluginLibraries);
					Expect.Call(file.ReadAllBytes(null)).IgnoreArguments().Return(bytes);
					Expect.Call(appDomain.CurrentDomain).Return(new AppDomainWrap(AppDomain.CurrentDomain));
					Expect.Call(appDomain.GetAssemblies()).Return(new[] { executingAssembly });
					Expect.Call(assemblyName.GetAssemblyName("fileName")).Repeat.Any().IgnoreArguments().Return(foundAssemblyName);
					Expect.Call(assembly.Load(null)).IgnoreArguments().Return(new AssemblyWrap(Assembly.GetExecutingAssembly()));
					Expect.Call(assembly.GetExecutingAssembly()).Return(executingAssembly);
					Expect.Call(assembly.GetTypes()).Return(new[] { typeof(TestBlogPlugin), typeof(TestNewsPlugin) });
					Expect.Call(() => repository.Delete(pluginId));
				})
				.Verify(() => result = service.UninstallPlugin(pluginId, PLUGINPATH));

			Assert.IsTrue(result);
		}



		private bool verifyInstallPlugin(string pluginName, With.IMockVerifier mocked)
		{
			var result = false;
			

			mocked.Verify(() =>
			              	{
			              		result = service.InstallPlugin(pluginName, PLUGINPATH);
			              	});

			return result;
		}

		private With.IMockVerifier mockFindAvailablePlugins(bool withSaveRepositoryCall)
		{
			var pluginLibraries = new []{ "plugins.dll" };

			var bytes = new byte[4];
			var foundAssemblyName = new AssemblyNameWrap("blog");
			var executingAssembly = mockery.Stub<IAssemblyWrap>();

			var mocked = With.Mocks(mockery)
				.Expecting(() =>
				           	{
				           		Expect.Call(directory.GetFiles(PLUGINPATH, PLUGINSEARCHPATTERN)).IgnoreArguments().Return(pluginLibraries);
				           		Expect.Call(file.ReadAllBytes(null)).IgnoreArguments().Return(bytes);
				           		Expect.Call(appDomain.CurrentDomain).Return(new AppDomainWrap(AppDomain.CurrentDomain));
				           		Expect.Call(appDomain.GetAssemblies()).Return(new[] {executingAssembly});
				           		Expect.Call(assemblyName.GetAssemblyName("fileName")).Repeat.Any().IgnoreArguments().Return(foundAssemblyName);
				           		Expect.Call(assembly.Load(null)).IgnoreArguments().Return(new AssemblyWrap(Assembly.GetExecutingAssembly()));
				           		Expect.Call(assembly.GetExecutingAssembly()).Return(executingAssembly);
				           		Expect.Call(assembly.GetTypes()).Return(new[] {typeof (TestBlogPlugin), typeof (TestNewsPlugin)});
								if (withSaveRepositoryCall)
									Expect.Call(repository.Save(null)).IgnoreArguments().Return(Guid.NewGuid());
				           	});
			return mocked;
		}
	}
}