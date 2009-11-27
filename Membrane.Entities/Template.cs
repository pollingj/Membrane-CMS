using Membrane.Commons.Plugin.Entities;

namespace Membrane.Entities
{
	public class Template : BaseEntity
	{
		public virtual string Name { get; set; }
		public virtual string Content { get; set; }
	}
}