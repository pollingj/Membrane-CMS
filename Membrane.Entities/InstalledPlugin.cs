using Membrane.Commons.Plugin.Entities;

namespace Membrane.Entities
{
	public class InstalledPlugin : BaseEntity
	{
		public virtual string Name { get; set; }
		public virtual string Version { get; set; }
	}
}