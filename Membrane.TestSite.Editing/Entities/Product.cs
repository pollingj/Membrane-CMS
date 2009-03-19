using System;
using System.Collections.Generic;
using Membrane.Commons.Persistence;

namespace Membrane.TestSite.Editing.Entities
{
	public class Product : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Price { get; set; }
		public virtual List<Accessory> Accessories { get; set; }
	}
}