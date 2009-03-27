using System;
using Membrane.Commons.Persistence;
using Membrane.Entities.Enums;

namespace Membrane.Entities
{
	public class MembraneUserType : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual UserType Type { get; set; }
	}
}