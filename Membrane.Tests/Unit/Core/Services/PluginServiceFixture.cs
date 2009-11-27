using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.Windsor;
using Membrane.Commons;
using Membrane.Commons.Persistence;
using Membrane.Commons.Wrappers.Interfaces;
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
		private IAssemblyLoader assemblyLoader;
		private IFileSystem fileSystem;
		private IWindsorContainer container;
		private IRepository<InstalledPlugin> repository;

		private IPluginsService service;

		private const string PLUGINPATH = "/plugins";
		private const string PLUGINSEARCHPATTERN = "*.dll";

		private Guid pluginId = Guid.NewGuid();
		private string[] pluginLibraries = new[] { "plugins.dll" };

		private byte[] bytes = new byte[4];
		private AssemblyName foundAssemblyName = new AssemblyName("blog");
		private Assembly executingAssembly;

		public override void SetUp()
		{
			base.SetUp();

			assemblyLoader = mockery.Stub<IAssemblyLoader>();
			fileSystem = mockery.Stub<IFileSystem>();
			container = mockery.DynamicMock<IWindsorContainer>();
			repository = mockery.DynamicMock<IRepository<InstalledPlugin>>();

			executingAssembly = Assembly.Load("Membrane.Commons");

			service = new PluginsService(assemblyLoader, fileSystem, container, repository);
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
			var result = false;



			With.Mocks(mockery)
				.Expecting(() =>
				{
					Expect.Call(repository.FindById(pluginId)).Return(new InstalledPlugin { Id = pluginId, Name = "Test Blog", Version = "2.0.0" });
					Expect.Call(fileSystem.GetFiles(PLUGINPATH, PLUGINSEARCHPATTERN)).IgnoreArguments().Return(pluginLibraries);
					Expect.Call(fileSystem.ReadAllBytes(null)).IgnoreArguments().Return(bytes);
					Expect.Call(assemblyLoader.GetCurrentDomainAssemblies()).Return(new[] { executingAssembly });
					Expect.Call(assemblyLoader.GetAssemblyName("fileName")).Repeat.Any().IgnoreArguments().Return(foundAssemblyName);
					Expect.Call(assemblyLoader.Load(null)).IgnoreArguments().Return(Assembly.GetExecutingAssembly());
					Expect.Call(assemblyLoader.GetExecutingAssembly()).Return(executingAssembly);
					Expect.Call(() => repository.Delete(pluginId));
				})
				.Verify(() => result = service.UninstallPlugin(pluginId, PLUGINPATH));

			Assert.IsTrue(result);
		}

		[Test]
		public void CanUpradePlugin()
		{
			var result = false;

			With.Mocks(mockery)
				.Expecting(() =>
           		{
					Expect.Call(repository.FindById(pluginId)).Return(new InstalledPlugin { Id = pluginId, Name = "Test Blog", Version = "2.0.0" });
					Expect.Call(fileSystem.GetFiles(PLUGINPATH, PLUGINSEARCHPATTERN)).IgnoreArguments().Return(pluginLibraries);
					Expect.Call(fileSystem.ReadAllBytes(null)).IgnoreArguments().Return(bytes);
					Expect.Call(assemblyLoader.GetCurrentDomainAssemblies()).Return(new[] { executingAssembly });
					Expect.Call(assemblyLoader.GetAssemblyName("fileName")).Repeat.Any().IgnoreArguments().Return(foundAssemblyName);
					Expect.Call(assemblyLoader.Load(null)).IgnoreArguments().Return(Assembly.GetExecutingAssembly());
					Expect.Call(assemblyLoader.GetExecutingAssembly()).Return(executingAssembly);
           			Expect.Call(() => repository.Update(new InstalledPlugin {Id = pluginId, Name = "Test Blog", Version = "3.0.0"})).IgnoreArguments();
           		})
				.Verify(() => result = service.UpgradePlugin(pluginId, PLUGINPATH));

			Assert.IsTrue(result);
		}

		[Test]
		public void CanRegisterPluginsWithContainer()
		{
			// TODO
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
			var mocked = With.Mocks(mockery)
				.Expecting(() =>
				           	{
				           		Expect.Call(fileSystem.GetFiles(PLUGINPATH, PLUGINSEARCHPATTERN)).IgnoreArguments().Return(pluginLibraries);
								Expect.Call(fileSystem.ReadAllBytes(null)).IgnoreArguments().Return(bytes);
				           		Expect.Call(assemblyLoader.GetCurrentDomainAssemblies()).Return(new[] {executingAssembly});
								Expect.Call(assemblyLoader.GetAssemblyName("fileName")).Repeat.Any().IgnoreArguments().Return(foundAssemblyName);
								Expect.Call(assemblyLoader.Load(null)).IgnoreArguments().Return(Assembly.GetExecutingAssembly());
								Expect.Call(assemblyLoader.GetExecutingAssembly()).Return(executingAssembly);
								//Expect.Call(assemblyLoader.GetTypes()).Return(new[] { typeof(TestBlogPlugin), typeof(TestNewsPlugin) });
								if (withSaveRepositoryCall)
									Expect.Call(repository.Save(null)).IgnoreArguments().Return(Guid.NewGuid());
				           	});
			return mocked;
		}
	}
}