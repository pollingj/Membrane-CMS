using System;
using Membrane.Commons.Persistence;
using Membrane.Entities.Enums;

namespace Membrane.Entities
{
	public class User : IEntity
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public UserType Type { get; set; }
	}
}