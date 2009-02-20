using System;
using Castle.ActiveRecord;

namespace Membrane.Core.Domain
{
	public class BaseModel
	{
		[PrimaryKey]
		public int Id { get; set; }

		[Property(NotNull = true)]
		public DateTime Created { get; set; }

		[Property(NotNull = true)]
		public DateTime LastModified { get; set; }

		[Property]
		public bool Delete { get; set; }
	}
}