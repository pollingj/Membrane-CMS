using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using SystemWrapper;
using SystemWrapper.IO;
using SystemWrapper.Reflection;
using Castle.Windsor;
using Membrane.Commons.Plugin.Services.Interfaces;
using Membrane.Commons.Wrappers.Interfaces;

namespace Membrane.Commons.Plugin.Services
{
	public class PluginsService : IPluginsService
	{
		private readonly IAssemblyWrap assembly;
		private readonly IAppDomainWrap appDomain;
		private readonly IAssemblyNameWrap assemblyName;
		private readonly IFileWrap file;
		private readonly IDirectoryWrap directory;
		private readonly IWindsorContainer container;

		protected static List<IAssemblyWrap> pluginAssemblies = new List<IAssemblyWrap>();
		private string pluginFolder = ConfigurationManager.AppSettings["plugins.path"];
		private Dictionary<string, AssemblyWrap> _assemblyList;


		public PluginsService(IAssemblyWrap assembly, IAppDomainWrap appDomain, IAssemblyNameWrap assemblyName, IFileWrap file, IDirectoryWrap directory)
		{
			this.assembly = assembly;
			this.appDomain = appDomain;
			this.assemblyName = assemblyName;
			this.file = file;
			this.directory = directory;
		}


		public List<IMembranePlugin> FindAvailablePlugins()
		{
			var pluginFilePaths = directory.GetFiles(pluginFolder, "*.dll");
			var foundPlugins = new List<IMembranePlugin>();

			foreach (var pluginFilePath in pluginFilePaths)
			{
				var pluginAssembly = getAssembly(pluginFilePath);

				if (pluginAssembly != assembly.GetExecutingAssembly())
				{
					try
					{
						var pluginTypes = pluginAssembly.GetTypes().Where(t => typeof(IMembranePlugin).IsAssignableFrom(t)).ToList();

						if (pluginTypes.Count > 0)
						{
							pluginAssemblies.Add(pluginAssembly);

							foreach (var pluginType in pluginTypes)
							{
								var plugin = (IMembranePlugin)Activator.CreateInstance(pluginType);

								plugin.Initialize();
								plugin.RegisterComponents(container);

								foundPlugins.Add(plugin);
							}
						}
					}
					catch (ReflectionTypeLoadException)
					{
						//There was a reflection error, ignore for now but probably need to at least log this info
					}
				}
			}

			return foundPlugins;
		}

		public bool InstallPlugin(string pluginName)
		{
			throw new System.NotImplementedException();
		}

		public bool UninstallPlugin(string pluginName)
		{
			throw new System.NotImplementedException();
		}

		public bool UpgradePlugin(string pluginName)
		{
			throw new System.NotImplementedException();
		}

		private IAssemblyWrap getAssembly(string fileName)
		{
			var currentDomain = appDomain.CurrentDomain;
			IAssemblyWrap[] assemblies = currentDomain.GetAssemblies();
			IAssemblyWrap foundAssembly = null;
			foreach (var assembly in assemblies)
			{
				if (assembly.FullName == assemblyName.GetAssemblyName(fileName).FullName)
				{
					foundAssembly = assembly;
					break;
				}
			}

			if (foundAssembly == null)
			{
				var assemblyBytes = file.ReadAllBytes(fileName);
				foundAssembly = assembly.Load(assemblyBytes);
			}

			return foundAssembly;
		}

		/// <summary>
		/// Resolves the dependancies when RegisterEntitiesAssembly is called for the plugin entities
		/// It searches the assemblyList stored earlier
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		/// <returns>The resolved Assembly</returns>
		IAssemblyWrap CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			IAssemblyWrap assembly = null;

			var name = args.Name;

			// Make sure the Name is clean to match the dictionary key
			if (name.IndexOf(',') > -1)
				name = name.Remove(name.IndexOf(','));

			if (_assemblyList.ContainsKey(name))
				assembly = _assemblyList[name];

			return assembly;

		}
	}
}