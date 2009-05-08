using Membrane.Commons.Persistence;

namespace Membrane.Commons.Plugin.Entities
{
	public class BaseVersionedAndOrderedEntity : BaseVersionedEntity, IOrderedEntity
	{
		public virtual int OrderPosition { get; set; }
	}
}