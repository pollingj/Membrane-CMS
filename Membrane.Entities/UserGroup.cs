using System;
using Membrane.Commons.Persistence;

namespace Membrane.Entities
{
	public class UserGroup : IEntity
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}