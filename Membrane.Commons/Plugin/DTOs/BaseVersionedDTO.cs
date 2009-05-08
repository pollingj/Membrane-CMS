using System;

namespace Membrane.Commons.Plug
{
	public class BaseVersionedDTO : BaseDTO
	{
		public virtual CultureDTO Culture { get; set; }
		public virtual int Revision { get; set; }
		public virtual Guid ParentEntity_Id { get; set; }
		public virtual bool Published { get; set; }
	}
}