using Membrane.Commons.Plugin.DTOs;

namespace Membrane.Core.DTOs
{
	public class InstalledPluginDTO : BaseDTO
	{
		public string Name { get; set; }
		public string Version { get; set; }
	}
}