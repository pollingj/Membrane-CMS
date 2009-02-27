using System;
using Membrane.Commons.Persistence;

namespace Membrane.Entities
{
	public class BaseModel : IEntity
	{
		public virtual int Id { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime LastModified { get; set; }
		public virtual bool Deleted { get; set; }
	}
}