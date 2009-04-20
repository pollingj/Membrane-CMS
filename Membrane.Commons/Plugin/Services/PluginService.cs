using Membrane.Commons.Wrappers.Interfaces;

namespace Membrane.Commons.Plugin.Services
{
	public class PluginService
	{
		private IAssembly assembly;
		private IDirectory directory;

		public PluginService(IAssembly assembly, IDirectory directory)
		{
			this.assembly = assembly;
			this.directory = directory;
		}
	}
}