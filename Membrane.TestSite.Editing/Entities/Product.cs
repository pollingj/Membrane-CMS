using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Membrane.Commons.Persistence;

namespace Membrane.TestSite.Editing.Entities
{
	[Export(typeof(IEntity))]
	public class Product : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Price { get; set; }
		public virtual List<Accessory> Accessories { get; set; }
	}
}