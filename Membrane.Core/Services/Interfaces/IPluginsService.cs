using System;
using System.Collections.Generic;
using Membrane.Commons;
using Membrane.Core.DTOs;

namespace Membrane.Core.Services.Interfaces
{
	public interface IPluginsService
	{
		IList<IMembranePlugin> FindAvailablePlugins(string pluginFolder);
		IList<InstalledPluginDTO> GetAllInstalledPlugins();
		bool InstallPlugin(string pluginName, string pluginFolder);
		bool UninstallPlugin(Guid id, string pluginFolder);
		bool UpgradePlugin(Guid id, string pluginFolder);
		void RegisterPlugin(string pluginName, string pluginFolder);
		void RegisterInstalledPlugins(string pluginFolder);
	}
}