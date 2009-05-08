using System;

namespace Membrane.Commons.Plugin.Entities
{
	public class BaseVersionedEntity : BaseEntity
	{
		public virtual Culture Culture { get; set; }
		public virtual int Revision { get; set; }
		public virtual Guid ParentEntity_Id { get; set; }
		public virtual bool Published { get; set; }
	}
}