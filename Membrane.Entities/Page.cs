using Membrane.Commons.Plugin.Entities;

namespace Membrane.Entities
{
	public class Page : BaseEntity
	{
		public virtual string Name { get; set; }
		public virtual string Content { get; set; }
		public virtual Template Template { get; set; }
	}
}