using System;
using Membrane.Commons.Persistence;
using Membrane.Entities.Enums;

namespace Membrane.Entities
{
	public class User : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual string Username { get; set; }
		public virtual string Password { get; set; }
		public virtual UserType Type { get; set; }
	}
}