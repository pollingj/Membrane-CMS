using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SystemWrapper;
using SystemWrapper.IO;
using SystemWrapper.Reflection;
using AutoMapper;
using Castle.Windsor;
using Membrane.Commons;
using Membrane.Commons.Persistence;
using Membrane.Commons.Persistence.Exceptions;
using Membrane.Core.DTOs;
using Membrane.Core.Queries.Plugin;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;

namespace Membrane.Core.Services
{
	public class PluginsService : IPluginsService
	{
		private readonly IAssemblyWrap assembly;
		private readonly IAppDomainWrap appDomain;
		private readonly IAssemblyNameWrap assemblyName;
		private readonly IFileWrap file;
		private readonly IDirectoryWrap directory;
		private readonly IWindsorContainer container;
		private readonly IRepository<InstalledPlugin> repository;

		protected static List<IAssemblyWrap> pluginAssemblies = new List<IAssemblyWrap>();
		private Dictionary<string, AssemblyWrap> _assemblyList;


		public PluginsService(IAssemblyWrap assembly, IAppDomainWrap appDomain, IAssemblyNameWrap assemblyName, IFileWrap file, IDirectoryWrap directory, IWindsorContainer container, IRepository<InstalledPlugin> repository)
		{
			this.assembly = assembly;
			this.appDomain = appDomain;
			this.assemblyName = assemblyName;
			this.file = file;
			this.directory = directory;
			this.container = container;
			this.repository = repository;
		}


		public IList<IMembranePlugin> FindAvailablePlugins(string pluginFolder)
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

								/*plugin.Initialize();
								plugin.RegisterComponents(container);*/

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

		public IList<InstalledPluginDTO> GetAllInstalledPlugins()
		{
			var installedPlugins = repository.Find(new OrderedPlugins());

			return Mapper.Map<ICollection<InstalledPlugin>, IList<InstalledPluginDTO>>(installedPlugins);
		}

		public bool InstallPlugin(string pluginName, string pluginFolder)
		{
			var installedPlugin = false;
			var id = Guid.Empty;
			var plugin = FindPlugin(pluginName, pluginFolder);

			if (plugin != null)
			{
				plugin.Install();
				plugin.Initialize();
				plugin.RegisterComponents(container);
				try
				{
					id = repository.Save(new InstalledPlugin {Name = plugin.Name, Version = plugin.Version});
				}
				catch (RepositorySaveException)
				{
					id = Guid.Empty;
				}

				if (id != Guid.Empty)
					installedPlugin = true;
			}

			return installedPlugin;
		}

		public bool UninstallPlugin(Guid id, string pluginFolder)
		{
			var foundPlugin = repository.FindById(id);
			var plugin = FindPlugin(foundPlugin.Name, pluginFolder);
			bool removed;

			plugin.Uninstall();
			plugin.RemoveComponents(container);

			try
			{
				repository.Delete(id);
				removed = true;
			}
			catch (RepositoryDeleteException)
			{
				removed = false;
			}


			return removed;
		}

		public bool UpgradePlugin(Guid id, string pluginFolder)
		{
			var foundPlugin = repository.FindById(id);
			var plugin = FindPlugin(foundPlugin.Name, pluginFolder);

			bool upgraded;

			foundPlugin.Version = plugin.Version;
			plugin.Upgrade();
			plugin.Initialize();
			plugin.RegisterComponents(container);

			try
			{
				repository.Update(foundPlugin);
				upgraded = true;
			}
			catch (RepositoryUpdateException)
			{
				upgraded = false;
			}

			return upgraded;
		}

		private IMembranePlugin FindPlugin(string pluginName, string pluginFolder)
		{
			IMembranePlugin foundPlugin = null;
			var installedPlugin = false;
			var plugins = FindAvailablePlugins(pluginFolder);
			Guid id = Guid.Empty;
			foreach (var plugin in plugins)
			{
				if (plugin.Name == pluginName)
				{
					foundPlugin = plugin;
					break;
				}
			}

			return foundPlugin;
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