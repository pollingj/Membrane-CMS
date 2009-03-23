using System;

namespace Membrane.Commons.Persistence
{
	public abstract class BaseModel : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime LastModified { get; set; }
		public virtual bool Deleted { get; set; }
	}
}