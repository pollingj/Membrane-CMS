using System;
using Membrane.Commons.Persistence;

namespace Membrane.Entities
{
	public abstract class BaseModel : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime LastModified { get; set; }
		public virtual bool Deleted { get; set; }
	}
}