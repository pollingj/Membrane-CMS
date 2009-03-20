using System;
using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Scaffolding;

namespace Membrane.TestSite.Editing.Entities
{
	public class Product : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual decimal Price { get; set; }
		[FieldTypeAttr(FieldType.MultipleDropDownList, typeof(Accessory), "Id", "Name")]
		public virtual List<Accessory> Accessories { get; set; }
	}
}