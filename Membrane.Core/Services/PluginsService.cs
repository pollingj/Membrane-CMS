using System.Collections.Generic;
using Membrane.Commons;
using Membrane.Core.Services.Interfaces;
using Membrane.Core.Wrappers.Interfaces;

namespace Membrane.Core.Services
{
	public class PluginsService : IPluginsService
	{
		private IDirectory directory;

		public PluginsService(IDirectory directory)
		{
			this.directory = directory;	
		}

		public List<IMembranePlugin> FindAvailablePlugins()
		{
			throw new System.NotImplementedException();
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
	}
}