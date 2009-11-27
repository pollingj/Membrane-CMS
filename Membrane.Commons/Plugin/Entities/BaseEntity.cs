using System;
using Membrane.Commons.Persistence;

namespace Membrane.Commons.Plugin.Entities
{
	public class BaseEntity : IEntity
	{
		public virtual Guid Id { get; set; }
	}
}