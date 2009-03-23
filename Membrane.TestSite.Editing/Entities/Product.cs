using System.Collections.Generic;
using Membrane.Commons.Persistence;
using Membrane.Commons.Scaffolding;

namespace Membrane.TestSite.Editing.Entities
{
	public class Product : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual decimal Price { get; set; }
		[FieldTypeAttr(FieldType.MultipleDropDownList, typeof(Accessory), "Id", "Name")]
		public virtual IList<Accessory> Accessories { get; set; }
	}
}