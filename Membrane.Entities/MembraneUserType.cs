using Membrane.Commons.Plugin.Entities;
using Membrane.Entities.Enums;

namespace Membrane.Entities
{
	public class MembraneUserType : BaseEntity
	{
		public virtual UserType Type { get; set; }
	}
}