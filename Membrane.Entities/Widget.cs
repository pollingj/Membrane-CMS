using System.Collections.Generic;
using Membrane.Commons.Persistence;

namespace Membrane.Entities
{
	public class Widget : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual IList<Page> Pages { get; set; }
	}
}