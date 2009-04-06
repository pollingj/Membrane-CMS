using System;
using Membrane.Commons.Persistence;

namespace Membrane.Entities
{
	public class MembraneUser : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual string Username { get; set; }
		public virtual string Password { get; set; }
		public virtual MembraneUserType Type { get; set; }
		public virtual UserGroup Group { get; set; }
	}
}