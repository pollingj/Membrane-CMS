using System.Collections.Generic;
using Membrane.Commons;

namespace Membrane.Core.Services.Interfaces
{
	public interface IPluginsService
	{
		List<IMembranePlugin> FindAvailablePlugins();
		bool InstallPlugin(string pluginName);
		bool UninstallPlugin(string pluginName);
		bool UpgradePlugin(string pluginName);
	}
}