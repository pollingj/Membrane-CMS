using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Scaffolding;

namespace Membrane.TestSite.Editing.Entities
{
	public class Accessory : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual decimal Price { get; set; }
		[FieldTypeAttr(FieldType.MultipleDropDownList, typeof(Product), "Id", "Name")]
		public virtual IList<Product> Products { get; set; }
	}
}