using Membrane.Commons.Persistence;

namespace Membrane.Commons.Plugin.Entities
{
	public class BaseOrderedEntity : BaseEntity, IOrderedEntity
	{
		public virtual int OrderPosition { get; set; }
	}
}