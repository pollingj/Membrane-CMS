using Membrane.Commons.Plugin.Entities;

namespace Membrane.Entities
{
	public class MembraneUser : BaseEntity
	{
		public virtual string Username { get; set; }
		public virtual string Password { get; set; }
		public virtual MembraneUserType Type { get; set; }
		public virtual UserGroup Group { get; set; }
		public virtual string Name { get; set; }
		public virtual string Email { get; set; }
	}
}