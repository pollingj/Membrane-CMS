using System.Collections.Generic;

namespace Membrane.Commons.Plugin.Services
{
	public interface IPluginsService
	{
		List<IMembranePlugin> FindAvailablePlugins();
		bool InstallPlugin(string pluginName);
		bool UninstallPlugin(string pluginName);
		bool UpgradePlugin(string pluginName);
	}
}