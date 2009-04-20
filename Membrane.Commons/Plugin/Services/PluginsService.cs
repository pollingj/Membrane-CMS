using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Castle.Windsor;
using Membrane.Commons.Wrappers.Interfaces;

namespace Membrane.Commons.Plugin.Services
{
	public class PluginsService : IPluginsService
	{
		private IAssemblyLoader assemblyLoader;
		private IFileSystem fileSystem;
		private IWindsorContainer container;

		protected static List<Assembly> pluginAssemblies = new List<Assembly>();
		private string pluginFolder = ConfigurationManager.AppSettings["plugins.path"];
		private Dictionary<string, Assembly> _assemblyList;

		public PluginsService(IAssemblyLoader assemblyLoader, IFileSystem fileSystem)
		{
			this.assemblyLoader = assemblyLoader;
			this.fileSystem = fileSystem;
		}


		public List<IMembranePlugin> FindAvailablePlugins()
		{
			var pluginFilePaths = fileSystem.GetFiles(pluginFolder, "*.dll");
			var foundPlugins = new List<IMembranePlugin>();

			foreach (var pluginFilePath in pluginFilePaths)
			{
				var pluginAssembly = getAssembly(pluginFilePath);

				if (pluginAssembly != assemblyLoader.GetExecutingAssembly())
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

		private Assembly getAssembly(string fileName)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			Assembly foundAssembly = null;
			foreach (Assembly assembly in assemblies)
			{
				//var assemblyName = ;
				if (assembly.FullName == assemblyLoader.GetAssemblyName(fileName).FullName)
				{
					foundAssembly = assembly;
					break;
				}
			}

			if (foundAssembly == null)
			{
				var assemblyBytes = fileSystem.ReadAllBytes(fileName);
				foundAssembly = Assembly.Load(assemblyBytes);
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
		Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			Assembly assembly = null;

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