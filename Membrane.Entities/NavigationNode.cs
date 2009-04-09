using System;
using Membrane.Commons.Persistence;

namespace Membrane.Entities
{
	public class NavigationNode : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
	}
}