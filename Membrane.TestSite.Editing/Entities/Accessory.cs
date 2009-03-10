using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Membrane.Commons.Persistence;

namespace Membrane.TestSite.Editing.Entities
{
	[Export(typeof(IEntity))]
	public class Accessory : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual double Price { get; set; }
		public virtual List<Product> Products { get; set; }
	}
}